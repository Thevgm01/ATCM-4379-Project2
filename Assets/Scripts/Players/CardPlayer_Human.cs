using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayer_Human : CardPlayer
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            hand.Add(draw.Draw());
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            discard.Add(hand.Draw());
        }

    }
}
