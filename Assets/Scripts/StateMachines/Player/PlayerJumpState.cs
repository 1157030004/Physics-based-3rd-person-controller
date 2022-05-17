using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shd.Player
{
    public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine stateMachine, float jumpTime) : base(stateMachine)
    {
        this.jumpTime = jumpTime;
    }

    float jumpHeight = 2f;
    float jumpTime;
    bool jumping;

    public override void Enter()
    {
        Debug.Log("Entering Jump State: " + jumpTime);
        // jumpTime = 0;
        // jumping = true;

        // if(stateMachine.IsInAir)
        // {
        //     stateMachine.SwitchState(new PLayerFreeLookState(stateMachine));
        // }
    }



    public override void FixedTick(float fixedDeltaTime)
    {
        

    }

    public override void Tick(float deltaTime)
    {
        Debug.Log("Ticking Jump State");
        float jumpForce = Mathf.Sqrt(-2 * Physics.gravity.y * jumpHeight);
        stateMachine.Rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        // jumpTime += Time.deltaTime;
        stateMachine.SwitchState(new PLayerFreeLookState(stateMachine));
    }

    public override void Exit()
    {
        Debug.Log("Exiting Jump State");
        // jumping = false;
    }
}
}
