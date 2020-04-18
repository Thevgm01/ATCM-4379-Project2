using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card
{
    public CardVisibility Visibility { get; private set; } = CardVisibility.None;

    public string Name { get; private set; } = "...";
    public int Rarity { get; private set; } = 0;
    public Sprite Graphic { get; private set; } = null;

    public List<CardEffect> PlayedEffects { get; private set; } = new List<CardEffect>();
    public List<CardEffect> DrawnEffects { get; private set; } = new List<CardEffect>();
    public List<CardEffect> DiscardedEffects { get; private set; } = new List<CardEffect>();

    protected Card(CardData data)
    {
        Name = data.Name;
        Rarity = data.Rarity;
        Graphic = data.Graphic;

        PlayedEffects = data.PlayedEffects;
        DrawnEffects = data.DrawnEffects;
        DiscardedEffects = data.DiscardedEffects;
    }

    public virtual void Play(CardPlayer player, ITargetable target)
    {
        Visibility = CardVisibility.Everyone;

        foreach(CardEffect effect in PlayedEffects)
        {
            if(effect != null)
            {
                effect.Activate(target);
            }  
        }
    }

    public virtual void Draw(CardPlayer player)
    {
        Visibility = CardVisibility.Player;

        foreach (CardEffect effect in DrawnEffects)
        {
            if (effect != null)
            {
                effect.Activate(player);
            }
        }
    }
    public virtual void Discard(CardPlayer player)
    {
        Visibility = CardVisibility.None;

        foreach (CardEffect effect in DiscardedEffects)
        {
            if (effect != null)
            {
                effect.Activate(player);
            }
        }
    }
}
