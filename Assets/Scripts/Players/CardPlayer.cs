using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayer : MonoBehaviour
{
    Deck<Card> draw, hand, discard;
    List<Ship> fleet;

    // Start is called before the first frame update
    void Start()
    {
        draw.Emptied += ShuffleOnEmpty;
    }

    void ShuffleOnEmpty()
    {
        discard.Shuffle();
        draw.MergeDeck(discard);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
