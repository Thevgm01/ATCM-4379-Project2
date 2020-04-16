using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageEffect", menuName = "Card/Effects/Damage")]
public class DamageEffect : CardEffect
{
    [SerializeField] int _damageAmount = 1;

    public override void Activate(Card targetCard)
    {
        // test to see if the target card is Damageable
        IDamageable objectToDamage = targetCard as IDamageable;
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
