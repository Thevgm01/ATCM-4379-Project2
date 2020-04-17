using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardPlayer
{
    public event Action<Card> DrewCard = delegate { };
    public event Action<Card> PlayedCard = delegate { };

    public Deck<AbilityCard> Hand { get; private set; } = new Deck<AbilityCard>();
    public bool IsAI { get; private set; }

    public CardPlayer(bool isAI)
    {
        IsAI = isAI;
    }

    public void PlayCard(int targetIndex)
    {
        AbilityCard targetCard = Hand.GetCard(targetIndex);
        targetCard.Play();
        // card should no longer exist in 'hand'
        Hand.Remove(targetIndex);
        // allow the next thing to grab it, if desired
        PlayedCard.Invoke(targetCard);
    }

    public void DrawAbilityCard(int numberToDraw, Deck<AbilityCard> targetDeck)
    {
        for (int i = 0; i < numberToDraw; i++)
        {
            AbilityCard newCard = targetDeck.Draw();
            if(newCard != null)
            {
                Hand.Add(newCard, DeckPosition.Top);
                Debug.Log("New Card: " + newCard.Name);
                DrewCard.Invoke(newCard);
            }
        }
    }
}
