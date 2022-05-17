using System;
using System.Collections;
using System.Collections.Generic;
using Shd;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shd.Player
{
    public class PlayerDodgeState : PlayerBaseState
    {
        // initiate constructor with Vector3 moveDirection
        public PlayerDodgeState(PlayerStateMachine stateMachine, Vector3 moveDirection, float moveAmount, float vertical, float horizontal) : base(stateMachine) 
        {
            this.moveDirection = moveDirection;
            this.moveAmount = moveAmount;
            this.vertical = vertical;
            this.horizontal = horizontal;
        }
        
        public AnimatorHandler animatorHandler;
        float timer;
        Vector3 moveDirection;
        float horizontal;
        float vertical;
        float moveAmount;
        float mouseX;
        float mouseY;

        public override void Enter()
        {
            // animatorHandler = new AnimatorHandler(stateMachine);
            stateMachine.IsInteracting = true;

            Debug.Log("Entering Dodge State");
        }
        public override void Tick(float deltaTime)
        {
            moveDirection = stateMachine.MainCameraTransform.forward * vertical;
            moveDirection += stateMachine.MainCameraTransform.right * horizontal;

            if(moveAmount > 0)
            {
                // stateMachine.PlayerAnimatorManager.PlayerTargetAnimation("HumanoidRoll", true, 0);
                stateMachine.AnimatorHandler.PlayerTargetAnimation("HumanoidRoll", true, 0);

                moveDirection.y = 0;
                Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                stateMachine.transform.rotation = rollRotation;
            }
            else
            {
                // stateMachine.PlayerAnimatorManager.PlayerTargetAnimation("HumanoidDodgeBack", true, 1);
                stateMachine.AnimatorHandler.PlayerTargetAnimation("HumanoidDodgeBack", true, 1);
                stateMachine.Rigidbody.AddForce(-stateMachine.transform.forward * 30f, ForceMode.Impulse);
                stateMachine.Rigidbody.velocity = Vector3.zero;
            }
            stateMachine.SwitchState(new PLayerFreeLookState(stateMachine));
        }

        public override void FixedTick(float fixedDeltaTime)
        {

        }
        public override void Exit()
        {
            stateMachine.IsInteracting = false;
            Debug.Log("Exiting Dodge State");
        }
    }
}
