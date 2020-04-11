using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Attribute
{
    None = 0,
    Brain,
    Muscle,
    Dexterity
}

public enum AbilityType
{
    None = 0,
    Attack,
    Buff,
    Summon
}

[CreateAssetMenu(fileName = "NewAbilityCardData", menuName = "CardData/Ability")]
public class AbilityCardData : CardData
{
    [Header("Ability Data")]
    [SerializeField] Attribute _attribute = Attribute.None;
    public Attribute Attribute => _attribute;

    [SerializeField] AbilityType _type = AbilityType.None;
    public AbilityType Type => _type;

    [SerializeField] int _cost = 0;
    public int Cost => _cost;

    [SerializeField] string _description = "...";
    public string Description => _description;

    [SerializeField] List<CardEffect> _cardEffects = new List<CardEffect>();
    public List<CardEffect> CardEffects => _cardEffects;

    [Header("Aesthetic Data")]

    [SerializeField] Sprite _graphic = null;
    public Sprite Graphic => _graphic;

    [SerializeField] AudioClip _playSFX = null;
    public AudioClip PlaySFX => _playSFX;
}
