using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card
{
    public enum CardVisibility
    {
        None,
        Player,
        Everyone
    }

    public CardVisibility Visibility { get; private set; } = CardVisibility.None;

    public virtual void Play()
    {
        Visibility = CardVisibility.Everyone;
    }

    public virtual void OnDraw()
    {
        Visibility = CardVisibility.Player;
    }
    public virtual void OnDiscard()
    {
        Visibility = CardVisibility.None;
    }
}
