using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTestController : MonoBehaviour
{
    [SerializeField] CardGameSM _cardGameSM = null;

    private void Update()
    {
        // Draw cards and move them into Discard
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AbilityCard newCard = _cardGameSM.DeckController.AbilityCardDeck.Draw();
            if(newCard != null)
            {
                _cardGameSM.DeckController.AbilityDiscardDeck.Add(newCard);
            }
            
        }
        // Enemy draws card
        if (Input.GetKeyDown(KeyCode.A))
        {

        }
        // Shuffle player deck
        if (Input.GetKeyDown(KeyCode.Z))
        {

        }
    }
}
