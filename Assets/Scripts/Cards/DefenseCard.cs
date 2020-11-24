using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseCard : Card
{
    public DefenseCardData Data { get; private set; }

    public DefenseCard(DefenseCardData cardData)
    {
        Data = cardData;
    }

    public void Play()
    {
        throw new System.NotImplementedException();
    }
}
