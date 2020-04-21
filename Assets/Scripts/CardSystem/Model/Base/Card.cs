using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card
{
    public CardVisibility Visibility { get; private set; } = CardVisibility.None;

    public virtual void Play(ITargetable target)
    {
        Visibility = CardVisibility.Everyone;
    }

    public virtual void Draw(CardPlayer player)
    {
        Visibility = CardVisibility.Player;


    }
    public virtual void Discard(CardPlayer player)
    {
        Visibility = CardVisibility.None;
    }
}
