using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardDiscardEffect : ScriptableObject
{
    public abstract void Activate(CardPlayer player);
}
