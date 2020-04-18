using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageEffect", menuName = "Card/Effects/Damage")]
public class DamageCardEffect : CardEffect
{
    [SerializeField] int _damageAmount = 1;

    public override void Activate(ITargetable target)
    {
        // test to see if the target is Damageable
        //TODO check this, to see if this syntax is correct
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
}
