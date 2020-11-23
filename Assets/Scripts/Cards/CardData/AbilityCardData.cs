using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability Card", menuName = "Card Data/Ability Card")]
public class AbilityCardData : CardData
{
    [SerializeField] int _cost = 1;
    public int Cost => _cost;
}