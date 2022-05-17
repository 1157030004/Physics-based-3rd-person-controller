using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Event OnTargetDestroyed
    public event Action<Target> OnDestroyed;

    void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
}
