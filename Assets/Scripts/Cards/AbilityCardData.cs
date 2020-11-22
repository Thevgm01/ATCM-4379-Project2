using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityCard", menuName = "CardData/AbilityCard")]
public class AbilityCardData : CardData
{
    [SerializeField] int _cost = 1;
    public int Cost => _cost;
}