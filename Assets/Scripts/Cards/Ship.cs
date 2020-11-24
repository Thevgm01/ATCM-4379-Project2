using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour, IDamageable
{
    public CardPlayer owner { get; private set; }
    List<Card> weapons, defenses;

    public int maxHitPoints;
    public int hitPoints;

    float baseEvasionChance;
    public float evasionChance;

    public int damageReduction;

    public int maxComponentSlots;
    public int componentSlots;

    void Awake()
    {
        weapons = new List<Card>();
        defenses = new List<Card>();
    }

    public void LoadData(CardPlayer owner, ShipCardData Data)
    {
        name = Data.Name;

        this.owner = owner;

        maxHitPoints = Data.MaxHitPoints;
        hitPoints = maxHitPoints;

        baseEvasionChance = Data.BaseEvasionChance;
        evasionChance = baseEvasionChance;

        damageReduction = 0;

        maxComponentSlots = Data.ComponentSlots;
        componentSlots = maxComponentSlots;
    }

    public bool TryAddWeapon(WeaponCard newWeapon)
    {
        int costToFire = ((WeaponCardData)newWeapon.Data).CostToFire;

        if (costToFire <= componentSlots)
        {
            weapons.Add(newWeapon);
            componentSlots -= costToFire;
            return true;
        }
        return false;
    }

    public void BreakRandomComponent()
    {
        if (weapons.Count == 0 && defenses.Count == 0) return;

        Card cardToBreak;

        int index = (int)Random.Range(0, weapons.Count + defenses.Count);
        if (index < weapons.Count)
        {
            cardToBreak = weapons[index];
        }
        else
        {
            cardToBreak = defenses[index - weapons.Count];
        }
    }

    public void TakeDamage(int num)
    {
        if(num > 0)
        {
            hitPoints -= num;
        }
    }
}
