using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCard : Card
{
    public AbilityCard(string name)
    {
        Name = name;
    }

    public override void Play()
    {
        Debug.Log("Playing ability card: " + Name);
    }
}
