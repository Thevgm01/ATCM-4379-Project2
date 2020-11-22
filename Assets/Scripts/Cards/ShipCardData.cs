using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShipCard", menuName = "CardData/ShipCard")]
public class ShipCardData : CardData
{
    [Header("Ship Info")]

    [SerializeField] GameObject _model = null;
    public GameObject Model => _model;

    [SerializeField] int _maxHitPoints = 1;
    public int MaxHitPoints => _maxHitPoints;

    [SerializeField] [Range(0f, 1f)] float _baseEvasionChance = 0.1f;
    public float BaseEvasionChance => _baseEvasionChance;

    [SerializeField] int _componentSlots = 3;
    public int ComponentSlots => _componentSlots;
}
