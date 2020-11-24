using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayer_Bot : CardPlayer
{
    void Evaluate()
    {
        if (!draw.IsEmpty)
        {
            Card newCard = draw.Draw();
            hand.Add(newCard);
            if (newCard is ShipCard) TryPlayCard(newCard, field.transform, field.transform.position);
        }
    }

    void Update()
    {
        Evaluate();
    }
}
