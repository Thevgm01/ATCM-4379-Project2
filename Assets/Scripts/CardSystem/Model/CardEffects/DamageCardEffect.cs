using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageEffect", menuName = "Card/Effects/Damage")]
public class DamageCardEffect : CardPlayEffect
{
    [SerializeField] int _damageAmount = 1;

    public override void Activate(ITargetable target)
    {
        // test to see if the target is Damageable
        IDamageable objectToDamage = target as IDamageable;
        // if it is, apply damage
        if(objectToDamage != null)
        {
            objectToDamage.TakeDamage(_damageAmount);
            Debug.Log("Add damage to card!");
        }
        else
        {
            Debug.Log("Card is not damageable...");
        }
    }

    public override bool IsTargetValid(ITargetable target)
    {
        // test to see if the target is Damageable
        IDamageable objectToDamage = target as IDamageable;

        if (objectToDamage != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
