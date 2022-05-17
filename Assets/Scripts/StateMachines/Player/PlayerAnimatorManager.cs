using System.Collections;
using System.Collections.Generic;
using Shd;
using UnityEngine;

namespace Shd.Player
{
    public class PlayerAnimatorManager : AnimatorManager
    {
        void Awake()
        {
            animator = GetComponent<Animator>();
        }
    }
}
