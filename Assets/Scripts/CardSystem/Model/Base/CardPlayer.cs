using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardPlayer
{
    public event Action<Card> DrewCard = delegate { };
    public event Action<Card> PlayedCard = delegate { };

    public Deck<Card> Hand { get; private set; }
    public bool IsAI { get; private set; }

    public CardPlayer(bool isAI)
    {
        IsAI = isAI;
    }

    public void PlayCard(int targetIndex)
    {
        Card targetCard = Hand.GetCard(targetIndex);
        targetCard.Play();
        // card should no longer exist in 'hand'
        Hand.Remove(targetIndex);
        // allow the next thing to grab it, if desired
        PlayedCard.Invoke(targetCard);
    }

    public void Draw(int numberToDraw, Deck<Card> targetDeck)
    {
        for (int i = 0; i < numberToDraw; i++)
        {
            Card newCard = targetDeck.Draw();
            Hand.Add(newCard, DeckPosition.Top);

            DrewCard.Invoke(newCard);
        }
    }
}
