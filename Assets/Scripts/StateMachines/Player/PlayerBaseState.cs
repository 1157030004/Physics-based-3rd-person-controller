using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shd.Player
{
    public abstract class PlayerBaseState : State
{
    
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(Vector3 movement, float deltaTime)
    {
        // stateMachine.Controller.Move((movement + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void HandleFloating()
    {
        // RaycastHit hit;
        Vector3 origin = new Vector3(stateMachine.transform.position.x, stateMachine.transform.position.y + stateMachine.OriginOffset, stateMachine.transform.position.z);
        stateMachine.RayDidHit = Physics.Raycast(origin, -Vector3.up, out stateMachine.objectHit, stateMachine.RayLength, stateMachine.PlatformLayer);
        if(stateMachine.RayDidHit)
        {
            stateMachine.IsGrounded = true;
            stateMachine.IsInteracting = false;
            stateMachine.IsInAir = false;
            Debug.DrawLine(origin, origin - Vector3.up * stateMachine.RayLength, Color.green);
        }
        else
        {
            stateMachine.IsGrounded = false;
            // stateMachine.IsInteracting = true;
            if(stateMachine.InAirTimer > stateMachine.CoyotePeriod)
            {
                stateMachine.IsInteracting = true;
                
            }
            stateMachine.IsInAir = true;
            Debug.DrawLine(origin, origin - Vector3.up * stateMachine.RayLength, Color.red);
        }

        if(stateMachine.RayDidHit)
        {
            Vector3 vel = stateMachine.Rigidbody.velocity;
            Vector3 rayDir = stateMachine.transform.TransformDirection(Vector3.down);

            Vector3 otherVel = Vector3.zero;
            Rigidbody hitBody = stateMachine.objectHit.rigidbody;
            
            if(hitBody != null)
            {
                otherVel = hitBody.velocity;
            }

            float rayDirVel = Vector3.Dot(rayDir, vel);
            float otherDirVel = Vector3.Dot(rayDir, otherVel);

            float relVel = rayDirVel - otherDirVel;
            float x = stateMachine.objectHit.distance - stateMachine.RideHeight;

            float springForce = (x * stateMachine.RideSpringStrength) - (relVel * stateMachine.RideSpringDamper);

            stateMachine.Rigidbody.AddForce(rayDir * springForce);

            if(hitBody != null)
            {
                hitBody.AddForceAtPosition(rayDir * -springForce, stateMachine.objectHit.point);
            }
        }
    }

    protected void FaceTarget()
    {
        if(stateMachine.Targeter.CurrentTarget == null) return;
        Vector3 targetPos = stateMachine.Targeter.CurrentTarget.transform.position;
        targetPos.y = 0f;
        Vector3 direction = targetPos - stateMachine.transform.position;
        stateMachine.transform.rotation = Quaternion.LookRotation(direction);
    }
}
}
