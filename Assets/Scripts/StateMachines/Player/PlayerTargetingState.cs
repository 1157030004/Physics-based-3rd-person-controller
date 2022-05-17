using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shd.Player
{
    public class PlayerTargetingState : PlayerBaseState
    {
    readonly int TargetingBlendTree = Animator.StringToHash("TargetingBlendTree");
    Vector3 moveDirection;
    float moveAmount;
    float m_MovementControlDisabledTimer;
    Vector3 m_UnitGoal;
    Vector3 m_GoalVel;
    Vector3 unitvel;
    Vector3 groundVel;

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) {}

        public override void Enter()
        {
            stateMachine.InputReader.CancelEvent += OnCancel;
            // stateMachine.PlayerAnimatorManager.animator.Play(TargetingBlendTree);
            stateMachine.AnimatorHandler.animator.Play(TargetingBlendTree);
        }

        public override void Tick(float deltaTime)
        {
            if(stateMachine.Targeter.CurrentTarget == null)
            {
                stateMachine.SwitchState(new PLayerFreeLookState(stateMachine));
                return;
            }
            FaceTarget();
        }

        

        public override void FixedTick(float fixedDeltaTime)
        {
            HandleMovement(fixedDeltaTime);
            // stateMachine.Rigidbody.AddForce(dir * stateMachine.TargetingSpeed * 25 * fixedDeltaTime);
        }

        public override void Exit()
        {
            stateMachine.InputReader.CancelEvent -= OnCancel;
        }

        void OnCancel()
        {
            stateMachine.Targeter.Cancel();
            stateMachine.SwitchState(new PLayerFreeLookState(stateMachine));
        }

        Vector3 CalculateMovement()
        {
            Vector3 moveDir = new Vector3();

            moveDir += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
            moveDir += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(moveDir.x) + Mathf.Abs(moveDir.y));
            return moveDir;
        }

        void HandleMovement(float fixedDeltaTime)
        {
            moveDirection = CalculateMovement();

            if(m_MovementControlDisabledTimer > 0)
            {
                moveDirection = Vector3.zero;
                m_MovementControlDisabledTimer -= fixedDeltaTime;
            }

            if(moveDirection.magnitude > 1.0f)
            {
                moveDirection.Normalize();
            }

            m_UnitGoal = moveDirection;

            Vector3 unitVel = m_GoalVel.normalized;

            float velDot = Vector3.Dot(m_UnitGoal, unitvel);

            float accel = stateMachine.Acceleration * stateMachine.AccelerationFactorFromDot.Evaluate(velDot);
        
            Vector3 goalVel = m_UnitGoal * stateMachine.TargetingSpeed * stateMachine.SpeedFactor;
            m_GoalVel = Vector3.MoveTowards(m_GoalVel, (goalVel) + (groundVel), accel * fixedDeltaTime);

            Vector3 neededAccel = (m_GoalVel - stateMachine.Rigidbody.velocity) / fixedDeltaTime;

            float maxAccel = stateMachine.MaxAccelForce * stateMachine.MaxAccelerationForceFactorFromDot.Evaluate(velDot) * stateMachine.MaxAccelForceFactor;

            neededAccel = Vector3.ClampMagnitude(neededAccel, maxAccel);
            stateMachine.Rigidbody.AddForce(Vector3.Scale(neededAccel * stateMachine.Rigidbody.mass, stateMachine.ForceScale));

            Vector3 tempMove = Vector3.ClampMagnitude(moveDirection, 1.0f);
            Debug.Log("TempMove: " +tempMove);
            stateMachine.AnimatorHandler.UpdateAnimatorValues(tempMove.x, tempMove.y, stateMachine.IsSprinting);
        }
    }
}