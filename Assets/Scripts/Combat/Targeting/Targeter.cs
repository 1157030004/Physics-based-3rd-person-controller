using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Shd.Combat
{
    public class Targeter : MonoBehaviour
    {
        [SerializeField] CinemachineTargetGroup cineTargetGroup;
        List<Target> targets = new List<Target>();
        public Target CurrentTarget { get; private set; }


        void OnTriggerEnter(Collider other)
        {
            if(!other.TryGetComponent<Target>(out Target target)) return;
                targets.Add(target);
                target.OnDestroyed += RemoveTarget;
        }

        void OnTriggerExit(Collider other)
        {
            if(!other.TryGetComponent<Target>(out Target target)) return;
                RemoveTarget(target);
        }

        public bool SelectTarget()
        {
            if(targets.Count == 0) return false;

            CurrentTarget =  targets[0];
            cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);

            return true;
        }

        public void Cancel()
        {
            if(CurrentTarget == null) return;

            cineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }

        void RemoveTarget(Target target)
        {
            if(CurrentTarget == target)
            {
                cineTargetGroup.RemoveMember(CurrentTarget.transform);
                CurrentTarget = null;
            }

            target.OnDestroyed -= RemoveTarget;
            targets.Remove(target);
        }
    }
}
