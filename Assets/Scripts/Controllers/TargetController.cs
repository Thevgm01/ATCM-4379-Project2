using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TargetController
{
    public event Action<ITargetable> TargetClicked = delegate { };

    public void ActivateNewTarget(ITargetable targetClicked)
    {
        TargetClicked.Invoke(targetClicked);
    }
    //public List<ITargetable> TargetList { get; private set; } = null;
}
