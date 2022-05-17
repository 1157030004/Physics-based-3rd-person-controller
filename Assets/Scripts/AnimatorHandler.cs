using Shd.Player;
using UnityEngine;

namespace Shd
{
    public class AnimatorHandler : AnimatorManager 
    {
        PlayerStateMachine playerStateMachine;
        int vertical;
        int horizontal;
     void Awake()
        {
            playerStateMachine = GetComponent<PlayerStateMachine>();
            animator = GetComponent<Animator>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
        {
            // Debug.Log("verticalMovement: " + verticalMovement + " horizontalMovement: " + horizontalMovement);
            #region vertical
            float v = 0;

            if(verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }
            else if(verticalMovement > 0.55f)
            {
                v = 1;
            }
            else if(verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if(verticalMovement < -0.55f)
            {
                v = -1;
            }
            else{
                v = 0;
            }
            #endregion

            #region horizontal
            float h = 0;    

            if(horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if(horizontalMovement > 0.55f)
            {
                h = 1;
            }
            else if(horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if(horizontalMovement < -0.55f)
            {
                h = -1;
            }
            else{
                h = 0;
            }
            #endregion

            if(isSprinting)
            {
                v = 2;
                h = horizontalMovement;
            }
            
            // Debug.Log("v: " + v + " h: " + h);
            animator.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            animator.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        public void CanRotate()
        {
            // CanRotate = true;
        }

    public void StopRotate()
    {
        // CanRotate = false;
    }   
    }
}