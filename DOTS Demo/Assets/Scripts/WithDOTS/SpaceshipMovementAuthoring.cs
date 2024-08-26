using UnityEngine;
using Unity.Entities;

public class SpaceshipMovementAuthoring : MonoBehaviour
{
    public float moveSpeed = 1000;

    private class Baker : Baker<SpaceshipMovementAuthoring>
    {
        public override void Bake(SpaceshipMovementAuthoring pAuthoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new SpaceshipMovementDOTS
            {
                moveSpeed = pAuthoring.moveSpeed,
            });
        }
    }
}

public struct SpaceshipMovementDOTS : IComponentData
{
    public float moveSpeed;
}
