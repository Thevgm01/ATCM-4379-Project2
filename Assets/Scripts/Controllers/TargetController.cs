using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TargetController
{
    public event Action<ITargetable> OnNewTarget = delegate { };

    public ITargetable CurrentTarget { get; private set; }

    public void SetNewTarget(ITargetable newTarget)
    {
        CurrentTarget = newTarget;
        newTarget.Target();

        OnNewTarget?.Invoke(newTarget);
    }
}
