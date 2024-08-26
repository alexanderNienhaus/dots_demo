using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Physics;
using Unity.Mathematics;
using Unity.Collections;
using System;
using UnityEngine.Events;

public partial class SpawnSpaceshipSystem : SystemBase
{
    public SpaceshipCountChangeEvent onSpaceshipCountChange;

    private NativeList<Entity> spaceshipInstanceList = new NativeList<Entity>(Allocator.Persistent);

    protected override void OnCreate() { }

    [BurstCompile]
    protected override void OnUpdate()
    {
        if (!Input.GetKeyDown(KeyCode.UpArrow) && !Input.GetKeyDown(KeyCode.DownArrow))
        {
            return;
        }

        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        NativeList<Entity> entitiesToDestroy = new NativeList<Entity>(Allocator.Persistent);

        foreach (RefRW<SpawnSpaceshipDOTS> spawnSpaceshipAuthoring in SystemAPI.Query<RefRW<SpawnSpaceshipDOTS>>())
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                for (int i = 0; i < spawnSpaceshipAuthoring.ValueRW.amountToSpawn; i++)
                {
                    TrySpawn(entityManager, spawnSpaceshipAuthoring);
                }
                onSpaceshipCountChange?.Invoke(spawnSpaceshipAuthoring.ValueRW.amountToSpawn);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && spaceshipInstanceList.Length >= spawnSpaceshipAuthoring.ValueRW.amountToSpawn)
            {
                for (int i = 0; i < spawnSpaceshipAuthoring.ValueRW.amountToSpawn; i++)
                {
                    entitiesToDestroy.Add(spaceshipInstanceList[spaceshipInstanceList.Length - 1]);
                    spaceshipInstanceList.RemoveAt(spaceshipInstanceList.Length - 1);
                }       
                onSpaceshipCountChange?.Invoke(-spawnSpaceshipAuthoring.ValueRW.amountToSpawn);
            }
        }

        for (int i = entitiesToDestroy.Length - 1; i >= 0; i--)
        {
            entityManager.DestroyEntity(entitiesToDestroy[i]);
        }        
    }

    [BurstCompile]
    private void TrySpawn(EntityManager pEntityManager, RefRW<SpawnSpaceshipDOTS> pSpawnSpaceshipAuthoring)
    {
        Vector3 randomPosInRange = new Vector3(
                UnityEngine.Random.Range(-pSpawnSpaceshipAuthoring.ValueRW.spawnRange, pSpawnSpaceshipAuthoring.ValueRW.spawnRange),
                UnityEngine.Random.Range(-pSpawnSpaceshipAuthoring.ValueRW.spawnRange, pSpawnSpaceshipAuthoring.ValueRW.spawnRange),
                UnityEngine.Random.Range(-pSpawnSpaceshipAuthoring.ValueRW.spawnRange, pSpawnSpaceshipAuthoring.ValueRW.spawnRange));

        float prefabScale = pEntityManager.GetComponentData<LocalTransform>(pSpawnSpaceshipAuthoring.ValueRW.spaceshipPrefab).Scale;
        float prefabVelocity = pEntityManager.GetComponentData<SpaceshipMovementDOTS>(pSpawnSpaceshipAuthoring.ValueRW.spaceshipPrefab).moveSpeed;

        if (!Physics.CheckBox(randomPosInRange, pSpawnSpaceshipAuthoring.ValueRW.colliderHalfExtents * prefabScale))
        {
            Entity spaceshipInstance = pEntityManager.Instantiate(pSpawnSpaceshipAuthoring.ValueRW.spaceshipPrefab);
            pEntityManager.SetComponentData(spaceshipInstance, new LocalTransform
            {
                Position = randomPosInRange,
                Rotation = quaternion.identity,
                Scale = prefabScale
            });

            pEntityManager.SetComponentData(spaceshipInstance, new PhysicsVelocity
            {
                Linear = prefabVelocity * SystemAPI.Time.DeltaTime * UnityEngine.Random.onUnitSphere.normalized
            });

            spaceshipInstanceList.Add(spaceshipInstance);
        }
        else
        {
            TrySpawn(pEntityManager, pSpawnSpaceshipAuthoring);
        }
    }
}

[Serializable]
public class SpaceshipCountChangeEvent : UnityEvent<int> { }
