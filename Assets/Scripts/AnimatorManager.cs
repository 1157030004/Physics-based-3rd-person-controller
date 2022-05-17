using UnityEngine;

namespace Shd
{
    public class AnimatorManager : MonoBehaviour 
    {
        public Animator animator;
        public void PlayerTargetAnimation(string targetAnimation, bool isInteracting, float duration)
        {
            animator.applyRootMotion = isInteracting;
            animator.SetBool("isInteracting", isInteracting);
            animator.CrossFade(targetAnimation, 0.2f);
            animator.PlayInFixedTime(targetAnimation, 0, duration);
            animator.applyRootMotion = !isInteracting;
        }
    }
}