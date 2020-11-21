using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckTest : MonoBehaviour
{
    Deck<Card> deck = new Deck<Card>();

    // Start is called before the first frame update
    void Start()
    {
        /*
        deck.Add(new Card("Fireball"));
        deck.Add(new Card("Sword"));
        deck.Add(new Card("Mace"));
        deck.Add(new Card("Helmet"));
        deck.Add(new Card("Gauntlet"));
        
        while (!deck.IsEmpty)
        {
            Card c = deck.Draw();
            c.Play();
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
