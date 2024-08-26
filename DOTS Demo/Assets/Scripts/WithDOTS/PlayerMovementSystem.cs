using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Physics;

public partial class PlayerMovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        foreach ((RefRO<LocalTransform> localTransform, RefRW<PlayerMovementDOTS> playerMovementDOTS, RefRW<PhysicsVelocity> physicsVelocity)
                    in SystemAPI.Query<RefRO<LocalTransform>, RefRW<PlayerMovementDOTS>, RefRW<PhysicsVelocity>>())
        {
            float timeMoveSpeed = playerMovementDOTS.ValueRW.moveSpeed * SystemAPI.Time.DeltaTime;

            float horizontalAxis = 0;
            float verticalAxis = 0;
            if (Input.GetKey(KeyCode.W))
            {
                verticalAxis = 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                verticalAxis = -1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                horizontalAxis = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                horizontalAxis = 1;
            }
            Vector3 right = Camera.main.transform.right;
            Vector3 forward = Camera.main.transform.forward;
            right.y = 0f;
            forward.y = 0f;
            right.Normalize();
            forward.Normalize();
            Vector3 desiredMoveDirection = right * horizontalAxis + forward * verticalAxis;
            physicsVelocity.ValueRW.Linear = desiredMoveDirection * timeMoveSpeed;

            if (Input.GetKey(KeyCode.Space))
            {
                physicsVelocity.ValueRW.Linear = Vector3.up * timeMoveSpeed;
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                physicsVelocity.ValueRW.Linear = Vector3.down * timeMoveSpeed;
            }

            if (Input.GetMouseButton(1))
            {
                playerMovementDOTS.ValueRW.yaw += playerMovementDOTS.ValueRW.speedH * Input.GetAxis("Mouse X");
                playerMovementDOTS.ValueRW.pitch -= playerMovementDOTS.ValueRW.speedV * Input.GetAxis("Mouse Y");

                Camera.main.transform.eulerAngles = new Vector3(playerMovementDOTS.ValueRW.pitch, playerMovementDOTS.ValueRW.yaw, 0.0f);
            }

            Camera.main.transform.position = localTransform.ValueRO.Position;
        }
    }
}
