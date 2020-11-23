using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCard : Card
{
    ShipCardData Data;

    public ShipCard(ShipCardData cardData)
    {
        Data = cardData;
    }

    public override void Play(Transform target)
    {
        Instantiate(Data.Model, target);
        throw new System.NotImplementedException();
    }
}
