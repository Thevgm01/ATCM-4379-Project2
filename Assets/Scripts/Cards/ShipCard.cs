using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCard : Card
{
    public ShipCardData Data { get; private set; }

    public ShipCard(ShipCardData cardData)
    {
        Data = cardData;
    }

    public override void Play()
    {
        throw new System.NotImplementedException();
    }
}
