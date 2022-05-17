using System.Collections;
using System.Collections.Generic;
using Shd.Combat;
using UnityEngine;

namespace Shd.Player
{
    public class PlayerStateMachine : StateMachine
{
    [field: Header("References")]
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    // [field: SerializeField] public PlayerAnimatorManager PlayerAnimatorManager { get; private set; }
    [field: SerializeField] public AnimatorHandler AnimatorHandler { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }

    [field: Header("Movement Stats")]
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float TargetingSpeed { get; private set; }
    [field: SerializeField] public float SprintingSpeed { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public float CoyotePeriod { get; private set; }
    [field: SerializeField] public float DodgeDelay { get; set; }
    [field: SerializeField] public float UprightJointSpringStrength { get; set; }
    [field: SerializeField] public float UprightJointSpringDamper { get; set; }
    [field: SerializeField] public float MaxSpeed { get; set; }
    [field: SerializeField] public float Acceleration { get; set; }
    [field: SerializeField] public float SpeedFactor { get; set; }
    [field: SerializeField] public float MaxAccelForceFactor { get; set; }
    [field: SerializeField] public AnimationCurve AccelerationFactorFromDot { get; set; }
    [field: SerializeField] public float MaxAccelForce { get; set; }
    [field: SerializeField] public AnimationCurve MaxAccelerationForceFactorFromDot { get; set; }
    [field: SerializeField] public Vector3 ForceScale { get; set; }



    [field: Header("Ground & Air Detection Stats")]
    [field: SerializeField] public float OriginOffset { get; set; }
    [field: SerializeField] public float RayLength { get; set; }
    [field: SerializeField] public float RideHeight { get; set; }
    [field: SerializeField] public float RideSpringStrength { get; set; }
    [field: SerializeField] public float RideSpringDamper { get; set; }
    
    [field: Header("States")]
    [field: SerializeField] public bool CanRotate { get; set; }
    [field: SerializeField] public bool IsSprinting { get; set; }
    [field: SerializeField] public bool IsInAir { get; set; }
    [field: SerializeField] public bool IsGrounded { get; set; }
    [field: SerializeField] public bool IsInteracting { get; set; }

    [field: SerializeField] public LayerMask PlatformLayer;

    public Vector3 SpawnLocation {get; set;}
    public bool RayDidHit {get; set;}
    public RaycastHit objectHit;
    public float InAirTimer {get; set;}



    public Transform MainCameraTransform { get; private set; }
    void Start()
    {
        MainCameraTransform = Camera.main.transform;
        SpawnLocation = transform.position;

        SwitchState(new PLayerFreeLookState(this));
    }
}
}
