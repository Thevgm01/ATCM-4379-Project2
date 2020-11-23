using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardData : ScriptableObject
{
    public enum TargetType
    {
        AllyField,
        AllyShip,
        AllAllyShips,
        EnemyField,
        EnemyShip,
        AllEnemyShips,
        FloatingCard,
    }

    [Header("Card Info")]

    [SerializeField] string _name = "...";
    public string Name => _name;

    [SerializeField] [TextArea] string _description = "...";
    public string Description => _description;

    [SerializeField] Sprite _graphic = null;
    public Sprite Graphic => _graphic;

    [SerializeField] TargetType _target = TargetType.AllyField;
    public TargetType Target => _target;
}