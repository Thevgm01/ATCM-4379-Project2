using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : ScriptableObject
{
    [Header("Card Data")]
    [SerializeField] string _name = "...";
    public string Name { get => _name; }

    [SerializeField] int _rarity = 0;
    public int Rarity { get => _rarity; }
    
    [SerializeField] Sprite _graphic = null;
    public Sprite Graphic => _graphic;

    [SerializeField] List<CardEffect> _drawnEffects = new List<CardEffect>();
    public List<CardEffect> DrawnEffects => _drawnEffects;

    [SerializeField] List<CardEffect> _playedEffects = new List<CardEffect>();
    public List<CardEffect> PlayedEffects => _playedEffects;

    [SerializeField] List<CardEffect> _discardedEffects = new List<CardEffect>();
    public List<CardEffect> DiscardedEffects => _discardedEffects;
}
