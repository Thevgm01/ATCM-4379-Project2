﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCard : ComponentCard
{
    public Ship installedShip;

    public WeaponCard(WeaponCardData cardData)
    {
        Data = cardData;
    }

    public void AttackShip(Ship other)
    {
        WeaponCardData weaponData = (WeaponCardData)Data;

        for(int i = 0; i < weaponData.NumberOfShots; ++i)
        {
            GameObject.Instantiate(
                weaponData.ParticleEffect,
                installedShip.transform.position, 
                Quaternion.LookRotation(other.transform.position - installedShip.transform.position, Vector3.up));

            bool hit =
                (weaponData.IgnoreEvasion || Random.Range(0, 1) > other.evasionChance) &&
                (weaponData.IgnoreDamageReduction || weaponData.Damage > other.damageReduction);

            bool breakComponent = Random.Range(0, 1) < weaponData.DestroyComponentChance;

            if (hit)
            {
                other.TakeDamage(weaponData.Damage);

                GameObject.Instantiate(
                    weaponData.HitEffect,
                    other.transform.position,
                    Quaternion.identity);
            }
            if (breakComponent) other.BreakRandomComponent();
        }
    }
}
