using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TargetController
{
    //TODO make a dictionary of targetables that we can use when we need
    public event Action<ITargetable> NewTargeted = delegate { };

    public List<ITargetable> PossibleTargets { get; private set; } = new List<ITargetable>();

    public ITargetable CurrentTarget => PossibleTargets[_currentTargetIndex];
    int _currentTargetIndex = 0;

    public void GetNextTarget()
    {
        int nextTargetIndex = ArrayHelper.GetNextLoopedIndex
            (_currentTargetIndex, PossibleTargets.Count);
        _currentTargetIndex = nextTargetIndex;

        NewTargeted.Invoke(CurrentTarget);
    }

    public void GetPreviousTarget()
    {
        int nextTargetIndex = ArrayHelper.GetPreviousLoopedIndex
            (_currentTargetIndex, PossibleTargets.Count);
        _currentTargetIndex = nextTargetIndex;

        NewTargeted.Invoke(CurrentTarget);
    }

    //TODO
    public void CreateTargets(List<ITargetable> newTargets)
    {
        
    }

    //TODO
    public void AddNewTarget(ITargetable newTarget)
    {
        PossibleTargets.Add(newTarget);
    }
}
