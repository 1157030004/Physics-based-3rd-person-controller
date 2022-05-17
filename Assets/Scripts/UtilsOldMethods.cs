using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilsOldMethods : MonoBehaviour
{
    #region handleMovement
    // void HandleMovement(float deltaTime)
    // {
    //     if(stateMachine.IsInteracting) return;

    //     moveDirection = stateMachine.MainCameraTransform.forward * vertical;
    //     moveDirection += stateMachine.MainCameraTransform.right * horizontal;
    //     moveDirection.Normalize();

    //     moveDirection.y = 0;

    //     float speed = stateMachine.MovementSpeed;

    //     if(stateMachine.InputReader.IsSprinting)
    //     {
    //         speed = stateMachine.SprintingSpeed;
    //     }

    //     moveDirection *= speed;

    //     Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
    //     stateMachine.Rigidbody.velocity = projectedVelocity;

    //     animatorHandler.UpdateAnimatorValues(moveAmount, 0, stateMachine.IsSprinting);

    //     if(stateMachine.CanRotate)
    //     {
    //         // HandleRotation(deltaTime);
    //         UpdateUprightForce(deltaTime);
    //     }

    //     // Move(moveDirection * stateMachine.MovementSpeed, deltaTime);
    // }
    #endregion

    #region Handle Falling

    //     void HandleFalling(float deltaTime)
    // {
    //     stateMachine.IsGrounded = false;
    //     RaycastHit hit;
    //     origin = stateMachine.transform.position;
    //     origin.y += stateMachine.GroundDetectionRayStartPoint;

    //     if(Physics.Raycast(origin, stateMachine.transform.forward, out hit, 0.4f))
    //     {
    //         moveDirection = Vector3.zero;
    //     }

    //     if(stateMachine.IsInAir)
    //     {
    //         stateMachine.Rigidbody.AddForce(-Vector3.up * stateMachine.FallingSpeed);
    //         stateMachine.Rigidbody.AddForce(moveDirection * stateMachine.FallingSpeed  / 20f);
    //     }

    //     Vector3 dir = moveDirection;
    //     dir.Normalize();
    //     origin = origin + dir * stateMachine.GroundDirectionRayDistance;

    //     targetPosition = stateMachine.transform.position;

    //     Debug.DrawRay(origin, -Vector3.up * stateMachine.MinimumDistanceNeededToBeginFall, Color.yellow, 0.1f, false);
    //     bool raycastHit = Physics.Raycast(origin, -Vector3.up, out hit, stateMachine.MinimumDistanceNeededToBeginFall, stateMachine.IgnoreForGroundCheck);
    //     // bool raycastHit = Physics.SphereCast(origin, 2f, -Vector3.up, out hit, stateMachine.MinimumDistanceNeededToBeginFall, stateMachine.IgnoreForGroundCheck);


    //     if(stateMachine.IsGrounded)
    //     {
    //         normalVector = hit.normal;
    //         Vector3 tp = hit.point;
    //         stateMachine.IsGrounded = true;
    //         targetPosition.y = tp.y;

    //         if(stateMachine.IsInAir)
    //         {
    //             if(inAirTimer > 0.5f)
    //             {
    //                 Debug.Log("You were in air for: "+ inAirTimer);
    //                 animatorHandler.PlayerTargetAnimation("HumanoidLand", true, 0);
    //                 inAirTimer = 0;
    //             }
    //             else
    //             {
    //                 animatorHandler.PlayerTargetAnimation("Locomotion", false, 0);
    //                 inAirTimer = 0;
    //             }

    //             stateMachine.IsInAir = false;

    //         }
    //     }
    //     else
    //     {
    //         if(stateMachine.IsGrounded)
    //         {
    //             stateMachine.IsGrounded = false;
    //         }

    //         if(stateMachine.IsInAir == false)
    //         {
    //             if(stateMachine.IsInteracting == false)
    //             {
    //                 animatorHandler.PlayerTargetAnimation("HumanoidFall", true, 0);
    //             }

    //             Vector3 vel = stateMachine.Rigidbody.velocity;
    //             vel.Normalize();
    //             stateMachine.Rigidbody.velocity = vel * (stateMachine.MovementSpeed / 2);
    //             stateMachine.IsInAir = true;
    //         }    
    //     }


    //     if(stateMachine.IsInteracting || moveAmount > 0)
    //     {
    //         stateMachine.transform.position = Vector3.Lerp(stateMachine.transform.position, targetPosition, deltaTime);
    //     }
    //     else
    //     {
    //         stateMachine.transform.position = targetPosition;
    //     }
    // }
    #endregion

    #region Handle Falling Sebastian Grave's Version
    // void FallingHandler()
    // {
    //     if(!isGrounded)
    //     {
    //         Debug.Log("Falling" + isGrounded);
    //         if(isInteracting == false)
    //         {
    //             isInteracting = true;
    //             AnimationHandler("HumanoidFall", 0);
    //             Debug.Log("Play Falling animation");
    //         }
            
    //         // float force = MathF.Sqrt(-2f * Physics.gravity.y * distance);
    //         rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass, ForceMode.Impulse);
    //         isInAir = true;
    //     }

    //     if(isGrounded)
    //     {
    //         if(isInAir)
    //         {
    //           if(inAirTime > 0.5f)
    //           {
    //             AnimationHandler("HumanoidLand", .25f);
    //             Debug.Log("Play Landing animation");
    //             isInteracting = false;
    //             inAirTime = 0f;
    //           }
    //           else if(inAirTime < 0.5f)
    //           {
    //             AnimationHandler("Locomotion", 0);
    //             Debug.Log("Play locomotion animation");
    //             isInteracting = false;
    //           }
    //         }
    //         isInAir = false;
    //         inAirTime = 0f;
    //     }

    //     // if(isInteracting )
    //     // {
    //     //     transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
    //     // }
    //     // else
    //     // {
    //     //     transform.position = targetPos;
    //     // }
    // }
    #endregion

    #region Handle Rotation
    //     void HandleRotation(float deltaTime)
    // {
    //     Vector3 targetDir = Vector3.zero;
    //     float moveOverride = moveAmount;

    //     Vector3 forward = stateMachine.MainCameraTransform.forward;
    //     Vector3 right = stateMachine.MainCameraTransform.right;

    //     targetDir = forward * vertical + right * horizontal;

    //     targetDir.Normalize();
    //     targetDir.y = 0;

    //     if(targetDir == Vector3.zero)
    //     {
    //         targetDir = stateMachine.transform.forward;
    //     }

    //     stateMachine.transform.rotation = Quaternion.Slerp(
    //         stateMachine.transform.rotation, 
    //         Quaternion.LookRotation(targetDir), 
    //         deltaTime * stateMachine.RotationSpeed);
    // }
    #endregion

    #region Handle Slope
    // void SlopeHandler(Vector3 origin)
    //  {

    //     RaycastHit hit;

    //     //  Vector3 origin = transform.position;

    //      if(Physics.SphereCast(origin, sphereCastRadius, Vector3.down, out hit, sphereCastDistance, IgnoreForGroundCheck))
    //      {
    //          groundSlopeAngle = Vector3.Angle(hit.normal, Vector3.up);

    //          Vector3 temp = Vector3.Cross(hit.normal, Vector3.down);
    //          groundSlopeDir = Vector3.Cross(temp, hit.normal);
    //          Debug.Log("Spherecast hit: " + hit.collider.name);
    //      }

    //     RaycastHit slopeHit1;
    //     RaycastHit slopeHit2;

    //     // First Raycast
    //     if(Physics.Raycast(origin + rayOriginOffset1, Vector3.down, out slopeHit1, raycastLength))
    //     {
    //         if(showDebug) { Debug.DrawLine(origin + rayOriginOffset1, slopeHit1.point, Color.red);}
    //         float angleOne = Vector3.Angle(slopeHit1.normal, Vector3.up);

    //         // 2ND Raycast
    //         if(Physics.Raycast(origin + rayOriginOffset2, Vector3.down, out slopeHit2, raycastLength))
    //         {
    //             if (showDebug) { Debug.DrawLine(origin + rayOriginOffset2, slopeHit2.point, Color.red); }
    //             float angleTwo = Vector3.Angle(slopeHit2.normal, Vector3.up);

    //              // 3 collision points: Take the MEDIAN by sorting array and grabbing middle.
    //             float[] tempArray = new float[] { groundSlopeAngle, angleOne, angleTwo };
    //             Array.Sort(tempArray);
    //             groundSlopeAngle = tempArray[1];
    //         }
    //         else
    //         {
    //             // 2 collision points (sphere and first raycast): AVERAGE the two
    //             float average = (groundSlopeAngle + angleOne) / 2;
	// 	        groundSlopeAngle = average;
    //         }
    //     }
    //  }
    #endregion

    #region Handle Height
    // void HeightHandler()
    // {
    //     RaycastHit hit;
    //     Vector3 origin = new Vector3(transform.position.x, transform.position.y - distanceFromBottom, transform.position.z);
    //     Physics.Raycast(origin, -Vector3.up, out hit);
    //     distance = (transform.position.y - hit.point.y) + distanceFromBottom;
    //     Debug.DrawRay(origin, Vector3.down * distance, Color.magenta);

    //     if(distance < 0)
    //     {
    //         distance = 0;
    //     }

    // }
    #endregion

    #region Ground Check Spherecast
    //     void GroundChecking()
    // {
        
    //     Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - distanceFromBottom, transform.position.z);
    //     isGrounded = Physics.CheckSphere(spherePosition, sphereCastRadius, IgnoreForGroundCheck,
    //             QueryTriggerInteraction.Ignore);

    //     // bool cast1 = Physics.CheckSphere(spherePosition, sphereCastRadius, IgnoreForGroundCheck, QueryTriggerInteraction.Ignore);
    //     bool cast2 = Physics.Raycast(spherePosition, Vector3.down, out RaycastHit hit, .7f, IgnoreForGroundCheck);
    //     float angle = Vector3.Angle(hit.normal, Vector3.up);
    //     targetPos = hit.point;

    //     if(angle > 0)
    //     {
    //         isGrounded = true;
    //         transform.position = targetPos;
    //     }
    // }
    #endregion

    #region Ground Check Spring Version
    // void RayDidHit()
    //     {
    //         RaycastHit hit;
    //         Vector3 origin = new Vector3(transform.position.x, transform.position.y + originOffset, transform.position.z);
    //         rayDidHit = Physics.Raycast(origin, -Vector3.up, out hit, rayLength, IgnoreForGroundCheck);
    //         if(rayDidHit)
    //         {
    //             isGrounded = true;
    //             Debug.DrawLine(origin, origin - Vector3.up * rayLength, Color.green);
    //         }
    //         else
    //         {
    //             isGrounded = false;
    //             Debug.DrawLine(origin, origin - Vector3.up * rayLength, Color.red);
    //         }

    //         if(rayDidHit)
    //         {
    //             Vector3 vel = rb.velocity;
    //             Vector3 rayDir = transform.TransformDirection(Vector3.down);

    //             Vector3 otherVel = Vector3.zero;
    //             Rigidbody hitBody = hit.rigidbody;
    //             if(hitBody != null)
    //             {
    //                 otherVel = hitBody.velocity;
    //             }

    //             float rayDirVel = Vector3.Dot(rayDir, vel);
    //             float otherDirVel = Vector3.Dot(rayDir, otherVel);

    //             float relVel = rayDirVel - otherDirVel;
    //             float x = hit.distance - rideHeight;

    //             float springForce = (x * rideSpringStrength) - (relVel * rideSpringDamper);

    //             rb.AddForce(rayDir * springForce);

    //             if(hitBody != null)
    //             {
    //                 hitBody.AddForceAtPosition(rayDir * -springForce, hit.point);
    //             }

    //         }
    //     }
    #endregion

    #region Simple Freefall
    // void Update()
    // {
    //     // if(verticalVelocity < 0f && controller.isGrounded)
    //     // {
    //     //     verticalVelocity = Mathf.Sqrt(2f * Physics.gravity.y * controller.height) * Time.deltaTime;
    //     // }
    //     // else
    //     // {
    //     //     verticalVelocity += Mathf.Sqrt(2f * Physics.gravity.y * controller.height) * Time.deltaTime;
    //     // }
    // }
    #endregion
}
