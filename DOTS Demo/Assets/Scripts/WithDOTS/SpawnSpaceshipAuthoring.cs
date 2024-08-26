using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections;

public class SpawnSpaceshipAuthoring : MonoBehaviour
{
    public GameObject spaceshipPrefab;
    public int amountToSpawn = 1000;
    public int spawnRange = 950;
    public Vector3 colliderHalfExtents = new Vector3(1800, 830, 2300);

    private class Baker : Baker<SpawnSpaceshipAuthoring>
    {
        public override void Bake(SpawnSpaceshipAuthoring pAuthoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new SpawnSpaceshipDOTS
            {
                spaceshipPrefab = GetEntity(pAuthoring.spaceshipPrefab, TransformUsageFlags.Dynamic),
                amountToSpawn = pAuthoring.amountToSpawn,
                spawnRange = pAuthoring.spawnRange,
                colliderHalfExtents = pAuthoring.colliderHalfExtents,
            });        
        }
    }
}

public struct SpawnSpaceshipDOTS : IComponentData
{
    public Entity spaceshipPrefab;
    public int amountToSpawn;
    public int spawnRange;
    public float3 colliderHalfExtents;
}
