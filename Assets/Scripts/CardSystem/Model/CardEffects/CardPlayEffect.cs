using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardPlayEffect : ScriptableObject
{
    [SerializeField] TargetType _targetType = TargetType.None;
    protected TargetType TargetType => _targetType;

    public abstract void Activate(ITargetable target);
}
