using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewKillEffect", menuName = "Card/Effects/Kill")]
public class KillCardEffect : CardPlayEffect
{
    public override void Activate(ITargetable target)
    {
        // test to see if the target is Damageable
        IDamageable objectToDamage = target as IDamageable;
        // if it is, apply damage
        if (objectToDamage != null)
        {
            objectToDamage.Kill();
            Debug.Log("Kill the target!");
        }
        else
        {
            Debug.Log("Card is not killable...");
        }
    }
}
