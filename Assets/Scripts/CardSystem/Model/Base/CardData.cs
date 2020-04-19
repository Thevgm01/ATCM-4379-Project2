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

    [SerializeField] DeckStyleData _deckStyle = null;
    public DeckStyleData DeckStyle => _deckStyle;

    [SerializeField] CardDrawEffect _drawEffect = null;
    public CardDrawEffect DrawEffect => _drawEffect;

    [SerializeField] CardPlayEffect _playEffect = null;
    public CardPlayEffect PlayEffect => _playEffect;

    [SerializeField] CardDiscardEffect _discardEffect = null;
    public CardDiscardEffect DiscardEffect => _discardEffect;
}
