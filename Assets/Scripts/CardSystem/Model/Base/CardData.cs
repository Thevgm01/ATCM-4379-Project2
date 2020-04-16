using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardData : ScriptableObject
{
    [Header("Card Data")]
    [SerializeField] string _name = "...";
    public string Name { get => _name; }

    [SerializeField] int _rarity = 0;
    public int Rarity { get => _rarity; }

    [SerializeField] List<CardEffect> _cardEffects = new List<CardEffect>();
    public List<CardEffect> CardEffects => _cardEffects;
}
