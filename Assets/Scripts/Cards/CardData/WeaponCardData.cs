using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Card", menuName = "Card Data/Weapon Card")]
public class WeaponCardData : ComponentCardData
{
    [Header("Weapon Info")]

    [SerializeField] TargetType _shootTarget = TargetType.EnemyShip;
    public TargetType ShootTarget => _shootTarget;

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

    [SerializeField] GameObject _particleEffect = null;
    public GameObject ParticleEffect => _particleEffect;

    [SerializeField] GameObject _hitEffect = null;
    public GameObject HitEffect => _hitEffect;

    [SerializeField] AudioClip _shootSound = null;
    public AudioClip ShootSound => _shootSound;

    [SerializeField] AudioClip _hitSound = null;
    public AudioClip HitSound => _hitSound;

}