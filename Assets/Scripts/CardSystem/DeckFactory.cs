using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeckFactory
{
    public static Deck<AbilityCard> CreateDeck(List<AbilityCardData> cardInfoList)
    {
        Deck<AbilityCard> deck = new Deck<AbilityCard>();

        foreach (AbilityCardData cardInfo in cardInfoList)
        {
            //Debug.Log("Add to deck: " + cardInfo.Name);
            deck.Add(CardFactory.CreateCard(cardInfo));
        }

        return deck;
    }
}
