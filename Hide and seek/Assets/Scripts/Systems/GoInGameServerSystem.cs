using Unity.Collections;
using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

namespace Systems
{
    [WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
    partial struct GoInGameServerSystem : ISystem
    {
        // [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            
            foreach ((RefRO<ReceiveRpcCommandRequest> receiveRpcCommandRequest, Entity entity) in
                     SystemAPI.Query<RefRO<ReceiveRpcCommandRequest>>().WithAll<GoInGameRequestRpc>().WithEntityAccess())
            {
                entityCommandBuffer.AddComponent<NetworkStreamInGame>(receiveRpcCommandRequest.ValueRO.SourceConnection);
                Debug.Log("Client Connected to Server");
                
                entityCommandBuffer.DestroyEntity(entity);
            }
            entityCommandBuffer.Playback(state.EntityManager);
        }
    } 
}

