using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityCardData", menuName = "Card/Data/Ability")]
public class AbilityCardData : CardData
{
    [Header("Gameplay Data")]
    [SerializeField] int _rarity = 0;
    public int Rarity { get => _rarity; }

    [SerializeField] CardAttribute _attribute = CardAttribute.None;
    public CardAttribute Attribute => _attribute;

    [SerializeField] CardType _type = CardType.None;
    public CardType Type => _type;

    [SerializeField] int _cost = 0;
    public int Cost => _cost;

    [Header("Ability Data")]
    [SerializeField] CardDrawEffect _drawEffect = null;
    public CardDrawEffect DrawEffect => _drawEffect;

    [SerializeField] CardPlayEffect _playEffect = null;
    public CardPlayEffect PlayEffect => _playEffect;

    [SerializeField] CardDiscardEffect _discardEffect = null;
    public CardDiscardEffect DiscardEffect => _discardEffect;

    [Header("Aesthetic Data")]
    [SerializeField] [TextArea] string _description = "...";
    public string Description => _description;

    [SerializeField] AudioClip _playSFX = null;
    public AudioClip PlaySFX => _playSFX;
}
