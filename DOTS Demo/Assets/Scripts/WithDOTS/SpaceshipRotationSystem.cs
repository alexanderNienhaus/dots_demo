using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Physics;

public partial struct SpaceshipRotationSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState pState)
    {
        SpaceshipRotationJob spaceshipRotationJob = new SpaceshipRotationJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        spaceshipRotationJob.ScheduleParallel();
    }
    
    [BurstCompile]
    public partial struct SpaceshipRotationJob : IJobEntity
    {
        public float deltaTime;

        public void Execute(ref LocalTransform pLocalTransform, in PhysicsVelocity pPhysicsVelocity, in SpaceshipMovementDOTS pSpaceshipMovementDOTS)
        {
            pLocalTransform.Rotation = Quaternion.LookRotation(pPhysicsVelocity.Linear, pLocalTransform.Up());
        }
    }
}
