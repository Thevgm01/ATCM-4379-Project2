using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComponentCardData : CardData
{
    [Header("Component Info")]

    [SerializeField] int _slotsRequired = 1;
    public int SlotsRequired => _slotsRequired;
}
