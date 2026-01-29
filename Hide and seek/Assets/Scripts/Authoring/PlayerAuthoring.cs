using UnityEngine;
using Unity.Entities;

namespace Authoring
{ 
    public class PlayerAuthoring : MonoBehaviour
    {
        public class PlayerAuthoringBaker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                
            }
        }
    }
}

