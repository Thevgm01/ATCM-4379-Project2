using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardFactory
{
    public static AbilityCard CreateCard(AbilityCardData cardData)
    {
        return new AbilityCard(cardData);
    }

    public static List<AbilityCard> CreateCards(List<AbilityCardData> cardInfoList)
    {
        List<AbilityCard> cards = new List<AbilityCard>();
        foreach (AbilityCardData cardInfo in cardInfoList)
        {
            cards.Add(CreateCard(cardInfo));
        }
        return cards;
    }
}

