using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityCardData", menuName = "Card/Data/Ability")]
public class AbilityCardData : CardData
{
    public enum CardAttribute
    {
        None = 0,
        Brain,
        Muscle,
        Dexterity
    }

    public enum CardType
    {
        None = 0,
        Attack,
        Buff,
        Summon
    }

    [Header("Ability Data")]
    [SerializeField] CardAttribute _attribute = CardAttribute.None;
    public CardAttribute Attribute => _attribute;

    [SerializeField] CardType _type = CardType.None;
    public CardType Type => _type;

    [SerializeField] int _cost = 0;
    public int Cost => _cost;

    [SerializeField] string _description = "...";
    public string Description => _description;

    [Header("Aesthetic Data")]

    [SerializeField] Sprite _graphic = null;
    public Sprite Graphic => _graphic;

    [SerializeField] AudioClip _playSFX = null;
    public AudioClip PlaySFX => _playSFX;
}
