using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComponentCard : Card
{
    public int SlotsRequired { get; protected set; } = 1;
}
