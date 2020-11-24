using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCard : ComponentCard
{
    public WeaponCard(WeaponCardData cardData)
    {
        Data = cardData;
    }

    public void AttackShip(Ship start, Ship other)
    {
        WeaponCardData weaponData = (WeaponCardData)Data;

        for(int i = 0; i < weaponData.NumberOfShots; ++i)
        {
            GameObject.Instantiate(
                weaponData.ParticleEffect, 
                start.transform.position, 
                Quaternion.LookRotation(other.transform.position - start.transform.position, Vector3.up));

            bool hit =
                (weaponData.IgnoreEvasion || Random.Range(0, 1) > other.evasionChance) &&
                (weaponData.IgnoreDamageReduction || weaponData.Damage > other.damageReduction);

            bool breakComponent = Random.Range(0, 1) < weaponData.DestroyComponentChance;

            if (hit) other.TakeDamage(weaponData.Damage);
            if (breakComponent) other.BreakRandomComponent();
        }
    }
}
