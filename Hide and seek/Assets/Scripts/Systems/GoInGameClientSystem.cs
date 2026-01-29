using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

namespace Systems
{
    [WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
    partial struct GoInGameClientSystem : ISystem
    {

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            foreach ((RefRO<NetworkId> networkId, Entity entity) 
                     in SystemAPI.Query<RefRO<NetworkId>>().WithNone<NetworkStreamInGame>().WithEntityAccess())
            {
                entityCommandBuffer.AddComponent<NetworkStreamInGame>(entity);
                Debug.Log("Setting client in game");
                
                Entity rpcEntity = entityCommandBuffer.CreateEntity();
                entityCommandBuffer.AddComponent(rpcEntity, new GoInGameRequestRpc());
                entityCommandBuffer.AddComponent(rpcEntity, new SendRpcCommandRequest());
            }
            
            entityCommandBuffer.Playback(state.EntityManager);
        }
    }
    
    public struct GoInGameRequestRpc : IRpcCommand
    {
        
    }
}
