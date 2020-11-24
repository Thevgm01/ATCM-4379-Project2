using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCard : Card
{
    public AbilityCardData Data { get; private set; }

    public AbilityCard(AbilityCardData cardData)
    {
        Data = cardData;
    }

    public void Play()
    {
        throw new System.NotImplementedException();
    }
}
