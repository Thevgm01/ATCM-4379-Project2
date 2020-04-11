using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardFactory
{
    /*
    public static Card CreateCard(CardType cardType, CardData data)
    {
        Card newCard;
        switch (cardType)
        {
            case CardType.Hero:
                newCard = CreateCreatureCard(data);
                break;
            case CardType.Ability:
                newCard = CreateAbilityCard(data);
                break;
            default:
                Debug.LogWarning("type not specified");
                newCard = null; //TODO change after testing
                break;
        }
        return newCard;
    }
    */

    public static AbilityCard CreateCard(AbilityCardData abilityData)
    {
        return new AbilityCard(abilityData);
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

