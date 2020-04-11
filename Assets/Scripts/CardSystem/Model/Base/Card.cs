using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card
{
    public bool RevealedToPlayer { get; protected set; }
    public bool RevealedToEveryone { get; protected set; }

    public virtual void Play()
    {
        RevealedToEveryone = true;
    }

    public virtual void OnDraw()
    {
        RevealedToPlayer = true;
    }
    public virtual void OnDiscard()
    {
        RevealedToPlayer = false;
        RevealedToEveryone = false;
    }
}
