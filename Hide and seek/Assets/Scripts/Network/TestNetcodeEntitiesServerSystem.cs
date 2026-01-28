using Unity.Burst;
using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
partial struct TestNetcodeEntitiesServerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }

    // [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
        
        foreach((
                    RefRO<TestRpc> testRpc, 
                    RefRO<ReceiveRpcCommandRequest> receiveRpcCommandRequest, Entity entity)
                in SystemAPI.Query<
                    RefRO<TestRpc>, 
                    RefRO<ReceiveRpcCommandRequest>>().WithEntityAccess())            
        {
            Debug.Log($"Received Rp: {testRpc.ValueRO.value} :: {receiveRpcCommandRequest.ValueRO.SourceConnection}"); // SourceConnection is the client that sent the RPC
            entityCommandBuffer.DestroyEntity(entity);
        }
        
        entityCommandBuffer.Playback(state.EntityManager);
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}
