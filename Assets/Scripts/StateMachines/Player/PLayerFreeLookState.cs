using System;
using System.Collections;
using System.Collections.Generic;
using Shd;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shd.Player
{
    public class PLayerFreeLookState : PlayerBaseState
{
    public PLayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    AnimatorHandler animatorHandler;
    readonly int Locomotion = Animator.StringToHash("Locomotion");
    Vector3 moveDirection;
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;
    float dodgeTimer;
    float m_MovementControlDisabledTimer;
    Vector3 m_UnitGoal;
    Vector3 m_GoalVel;
    Vector3 unitvel;
    Vector3 groundVel;
    float jumpHeight = 4f;
    float coyoteTimeCounter;

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.InputReader.DodgeEvent += OnDodge;
        stateMachine.InputReader.SprintStartEvent += OnSprintStart;
        stateMachine.InputReader.SprintEndEvent += OnSprintEnd;
        stateMachine.InputReader.ResetEvent += OnReset;
        stateMachine.InputReader.JumpEvent += OnJump;

        // stateMachine.PlayerAnimatorManager.animator.Play(Locomotion);
        stateMachine.AnimatorHandler.animator.Play(Locomotion);
    }

    public override void Tick(float deltaTime)
    {
        CalculateMovement();
        stateMachine.IsInteracting = stateMachine.IsInteracting;
        dodgeTimer += deltaTime;

        if(stateMachine.IsInAir)
        {
            stateMachine.InAirTimer += deltaTime;
        }

        if(stateMachine.IsGrounded)
        {
            coyoteTimeCounter = stateMachine.CoyotePeriod;
            stateMachine.InAirTimer = 0;
        }
        else
        {
            coyoteTimeCounter -=deltaTime;
        }
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        Movement(fixedDeltaTime);
        HandleFloating();


    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.InputReader.DodgeEvent += OnDodge;
        stateMachine.InputReader.SprintStartEvent += OnSprintStart;
        stateMachine.InputReader.SprintEndEvent += OnSprintEnd;
        stateMachine.InputReader.ResetEvent += OnReset;
        stateMachine.InputReader.JumpEvent += OnJump;
    }

    private void OnJump()
    {
        stateMachine.IsInteracting = true;

        if(coyoteTimeCounter < 0f) return;

        float jumpForce = Mathf.Sqrt(-2 * Physics.gravity.y * jumpHeight);
        stateMachine.Rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);

        coyoteTimeCounter = 0f;
    }

    private void OnReset()
    {
        stateMachine.transform.position = stateMachine.SpawnLocation;
        // stateMachine.PlayerAnimatorManager.PlayerTargetAnimation("Empty", false, 0);
        stateMachine.AnimatorHandler.PlayerTargetAnimation("Empty", false, 0);
    }

    void OnSprintStart()
    {
        stateMachine.IsSprinting = true;
    }

    void OnSprintEnd()
    {
        stateMachine.IsSprinting = false;
    }

    void OnTarget()
    {
        if(!stateMachine.Targeter.SelectTarget()) return;
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    void OnDodge()
    {
        if(dodgeTimer < Mathf.Abs(stateMachine.DodgeDelay)) return;
        stateMachine.IsInteracting = true;
        stateMachine.SwitchState(new PlayerDodgeState(stateMachine, moveDirection, moveAmount, vertical, horizontal));
        dodgeTimer = 0;
    }

    void HandleRotation(float deltaTime)
    {
        Vector3 targetDir = Vector3.zero;
        float moveOverride = moveAmount;

        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        targetDir = forward * vertical + right * horizontal;

        targetDir.Normalize();
        targetDir.y = 0;

        if(targetDir == Vector3.zero)
        {
            targetDir = stateMachine.transform.forward;
        }

        stateMachine.transform.rotation = Quaternion.Slerp(
            stateMachine.transform.rotation, 
            Quaternion.LookRotation(targetDir), 
            deltaTime * stateMachine.RotationSpeed);
    }

    void UpdateUprightForce()
    {
        Vector3 targetDir = Vector3.zero;
        float moveOverride = moveAmount;

        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        targetDir = forward * vertical + right * horizontal;

        targetDir.Normalize();
        targetDir.y = 0;

        if(targetDir == Vector3.zero)
        {
            targetDir = stateMachine.transform.forward;
        }

        Quaternion uprightJointTargetRot = Quaternion.LookRotation(targetDir);
        Quaternion characterCurrent = stateMachine.transform.rotation;
        Quaternion toGoal = UtilsMath.ShorterstRotation(uprightJointTargetRot, characterCurrent);

        Vector3 rotAxis;
        float rotDegrees;

        toGoal.ToAngleAxis(out rotDegrees, out rotAxis);
        rotAxis.Normalize();

        float rotRadians = rotDegrees * Mathf.Deg2Rad;

        stateMachine.Rigidbody.AddTorque((rotAxis * (rotRadians * stateMachine.UprightJointSpringStrength)) - (stateMachine.Rigidbody.angularVelocity * stateMachine.UprightJointSpringDamper));
    }

    void Movement(float fixedDeltaTime)
    {
        Debug.Log("interacting Movement: " +stateMachine.IsInteracting);
        if(stateMachine.IsInteracting) return;

        moveDirection = stateMachine.MainCameraTransform.forward * vertical;
        moveDirection += stateMachine.MainCameraTransform.right * horizontal;

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

        // Calculate new goal velocity
        Vector3 unitVel = m_GoalVel.normalized;

        float velDot = Vector3.Dot(m_UnitGoal, unitvel);

        float accel = stateMachine.Acceleration * stateMachine.AccelerationFactorFromDot.Evaluate(velDot);

        // float speed = stateMachine.MaxSpeed;

        Vector3 goalVel = m_UnitGoal * stateMachine.MaxSpeed * stateMachine.SpeedFactor;

        if(stateMachine.IsSprinting)
        {
            goalVel = m_UnitGoal * stateMachine.SprintingSpeed * stateMachine.SpeedFactor;
        }
        m_GoalVel = Vector3.MoveTowards(m_GoalVel, (goalVel) + (groundVel), accel * fixedDeltaTime);

        // Actual force
        Vector3 neededAccel = (m_GoalVel - stateMachine.Rigidbody.velocity) / fixedDeltaTime;

        float maxAccel = stateMachine.MaxAccelForce * stateMachine.MaxAccelerationForceFactorFromDot.Evaluate(velDot) * stateMachine.MaxAccelForceFactor;

        neededAccel = Vector3.ClampMagnitude(neededAccel, maxAccel);
        stateMachine.Rigidbody.AddForce(Vector3.Scale(neededAccel * stateMachine.Rigidbody.mass, stateMachine.ForceScale));
    
        stateMachine.AnimatorHandler.UpdateAnimatorValues(moveAmount, 0, stateMachine.IsSprinting);

        if(stateMachine.CanRotate)
        {
            // UpdateUprightForce();
            HandleRotation(Time.deltaTime);
        }
    }

    void CalculateMovement()
    {
        horizontal = stateMachine.InputReader.MovementValue.x;
        vertical = stateMachine.InputReader.MovementValue.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = stateMachine.InputReader.CameraValue.x;
        mouseY = stateMachine.InputReader.CameraValue.y;

    }

    void HandleGliding(float deltaTime)
    {
        stateMachine.Rigidbody.drag = 6;
        Vector3 localV = stateMachine.transform.InverseTransformDirection(stateMachine.Rigidbody.velocity);
        localV.z = 12.5f;
        stateMachine.Rigidbody.velocity = stateMachine.transform.TransformDirection(localV);
    }


}
}
