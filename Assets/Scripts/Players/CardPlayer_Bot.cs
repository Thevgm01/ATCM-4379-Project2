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
                if (TryPlayCard(c, field.transform, field.transform.position, false))
                    return true;
            }
            else if (c is WeaponCard)
            {
                if (TryPlayCard(c, RandomShip().transform, field.transform.position, false))
                    return true;
            }
            else if (c is AbilityCard)
            {
                if (TryPlayCard(c, opponent.RandomShip().transform, Vector3.zero, false))
                    return true;
            }
        }

        return false;
    }
}
