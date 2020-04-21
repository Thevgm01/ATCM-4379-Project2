using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilityCard : Card, ITargetable
{
    public string Name { get; private set; } = "...";
    public int Rarity { get; private set; } = 0;
    public Sprite Graphic { get; private set; } = null;

    public int Cost { get; private set; }
    public string Description { get; private set; }
    public CardAttribute Attribute { get; private set; }
    public Sprite FrontImage { get; private set; }
    public Sprite BackImage { get; private set; }

    public CardPlayEffect PlayEffect { get; private set; }
    public CardDrawEffect DrawEffect { get; private set; }
    public CardDiscardEffect DiscardEffect { get; private set; }

    // constructor, Awake, Start
    public AbilityCard(AbilityCardData data)
    {
        Name = data.Name;
        Rarity = data.Rarity;
        Graphic = data.Graphic;

        PlayEffect = data.PlayEffect;
        DrawEffect = data.DrawEffect;
        DiscardEffect = data.DiscardEffect;

        Cost = data.Cost;
        Description = data.Description;
        Attribute = data.Attribute;
    }

    // public methods
    public override void Play(ITargetable target)
    {
        Debug.Log("PLAY Ability Card: " + Name);
        base.Play(target);

        if (PlayEffect != null)
        {
            PlayEffect.Activate(target);
        }
    }

    public override void Discard(CardPlayer player)
    {
        base.Discard(player);

        if (DrawEffect != null)
        {
            DrawEffect.Activate(player);
        }
    }

    public override void Draw(CardPlayer player)
    {
        base.Draw(player);

        if (DiscardEffect != null)
        {
            DiscardEffect.Activate(player);
        }
    }

    public void Target()
    {
        Debug.Log("Card " + Name + " was targeted.");
    }
}
