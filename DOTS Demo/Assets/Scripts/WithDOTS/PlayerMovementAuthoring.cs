using UnityEngine;
using Unity.Entities;

public class PlayerMovementAuthoring : MonoBehaviour
{
    public float moveSpeed = 100000f;
    public float speedH = 10f;
    public float speedV = 10f;
    
    public float yaw = 0.0f;
    public float pitch = 0.0f;

    private class Baker : Baker<PlayerMovementAuthoring>
    {
        public override void Bake(PlayerMovementAuthoring pAuthoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerMovementDOTS
            {
                moveSpeed = pAuthoring.moveSpeed,
                speedH = pAuthoring.speedH,
                speedV = pAuthoring.speedV,
                yaw = 0,
                pitch = 0
            });
        }
    }
}

public struct PlayerMovementDOTS : IComponentData
{
    public float moveSpeed;
    public float speedH;
    public float speedV;

    public float yaw;
    public float pitch;
}
