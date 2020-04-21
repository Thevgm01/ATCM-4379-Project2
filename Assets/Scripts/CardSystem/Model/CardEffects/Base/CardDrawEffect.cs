using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardDrawEffect : ScriptableObject
{
    public abstract void Activate(CardPlayer player);
}
