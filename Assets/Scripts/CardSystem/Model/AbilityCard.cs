using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilityCard : Card, ITargetable
{
    public int Cost { get; private set; }
    public string Description { get; private set; }
    public CardAttribute Attribute { get; private set; }

    // constructor, Awake, Start
    public AbilityCard(AbilityCardData data) : base(data)
    {
        Cost = data.Cost;
        Description = data.Description;
        Attribute = data.Attribute;
    }

    // public methods
    public override void Play(CardPlayer player, ITargetable target)
    {
        Debug.Log("PLAY Ability Card: " + Name);
        base.Play(player, target);
    }

    public override void Discard(CardPlayer player)
    {
        base.Discard(player);
        //
    }

    public override void Draw(CardPlayer player)
    {
        base.Draw(player);
        //
    }

    public void Target()
    {
        Debug.Log("Card " + Name + " was targeted.");
    }
}
