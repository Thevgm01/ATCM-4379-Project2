using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card
{
    public CardVisibility Visibility { get; private set; } = CardVisibility.None;

    public string Name { get; private set; } = "...";
    public int Rarity { get; private set; } = 0;
    public Sprite Graphic { get; private set; } = null;

    public CardPlayEffect PlayEffect { get; private set; }
    public CardDrawEffect DrawEffect { get; private set; }
    public CardDiscardEffect DiscardEffect { get; private set; }

    protected Card(CardData data)
    {
        Name = data.Name;
        Rarity = data.Rarity;
        Graphic = data.Graphic;

        PlayEffect = data.PlayEffect;
        DrawEffect = data.DrawEffect;
        DiscardEffect = data.DiscardEffect;
    }

    public virtual void Play(ITargetable target)
    {
        Visibility = CardVisibility.Everyone;

        if(PlayEffect != null)
        {
            PlayEffect.Activate(target);
        }  
    }

    public virtual void Draw(CardPlayer player)
    {
        Visibility = CardVisibility.Player;

        if (DrawEffect != null)
        {
            DrawEffect.Activate(player);
        }
    }
    public virtual void Discard(CardPlayer player)
    {
        Visibility = CardVisibility.None;

        if (DiscardEffect != null)
        {
            DiscardEffect.Activate(player);
        }
    }
}
