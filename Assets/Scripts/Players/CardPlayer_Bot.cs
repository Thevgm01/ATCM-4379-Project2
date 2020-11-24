using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayer_Bot : CardPlayer
{
    public CardPlayer opponent;

    public bool Evaluate()
    {
        foreach(Card c in hand)
        {
            if (c is ShipCard)
            {
                TryPlayCard(c, field.transform, field.transform.position);
                return true;
            }
            else if (c is WeaponCard)
            {
                TryPlayCard(c, RandomShip().transform, field.transform.position);
                return true;
            }
            else if (c is AbilityCard)
            {
                if (energy > 3)
                {
                    TryPlayCard(c, opponent.RandomShip().transform, Vector3.zero);
                    return true;
                }
            }
        }

        return false;
    }
}
