using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class CardEffect
{
    public abstract void Activate(Card card);
}
