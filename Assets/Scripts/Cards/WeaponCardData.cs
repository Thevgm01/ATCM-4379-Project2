using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponCard", menuName = "CardData/WeaponCard")]
public class WeaponCardData : CardData
{
    public enum TargetType
    {
        Single,
        All
    }

    [Header("Weapon Info")]

    [SerializeField] TargetType _target = TargetType.Single;
    public TargetType Target => _target;

    //[SerializeField] [Range(0f, 1f)] float _chanceToHit = 1;
    //public float ChanceToHit => _chanceToHit;

    [SerializeField] bool _ignoreEvasion = false;
    public bool IgnoreEvasion => _ignoreEvasion;

    [SerializeField] int _damage = 1;
    public int Damage => _damage;

    [SerializeField] bool _ignoreDamageReduction = false;
    public bool IgnoreDamageReduction => _ignoreDamageReduction;

    [SerializeField] [Range(0f, 1f)] float _destroyComponentLikelihood = 0;
    public float DestroyComponentLikelihood => _destroyComponentLikelihood;
}