using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Card", menuName = "Card Data/Weapon Card")]
public class WeaponCardData : ComponentCardData
{
    [Header("Weapon Info")]

    //[SerializeField] [Range(0f, 1f)] float _chanceToHit = 1;
    //public float ChanceToHit => _chanceToHit;

    [SerializeField] bool _ignoreEvasion = false;
    public bool IgnoreEvasion => _ignoreEvasion;

    [SerializeField] int _damage = 1;
    public int Damage => _damage;

    [SerializeField] int _numberOfShots = 1;
    public int NumberOfShots => _numberOfShots;

    [SerializeField] bool _ignoreDamageReduction = false;
    public bool IgnoreDamageReduction => _ignoreDamageReduction;

    [SerializeField] [Range(0f, 1f)] float _destroyComponentChance = 0;
    public float DestroyComponentChance => _destroyComponentChance;

    [SerializeField] int _costToFire = 1;
    public int CostToFire => _costToFire;
}