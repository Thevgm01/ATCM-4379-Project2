using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardPlayer : ITargetable, IDamageable
{
    public event Action<AbilityCard> DrewCard = delegate { };
    public event Action<AbilityCard> PlayedCard = delegate { };

    public Deck<AbilityCard> Hand { get; private set; } = new Deck<AbilityCard>();
    public bool IsAI { get; private set; }
    public string Name { get; private set; }

    public AbilityCard CurrentSelectedCard => Hand.GetCard(_selectedCardIndex);
    public ITargetable CurrentTarget { get; private set; }

    int _selectedCardIndex = 0;

    public CardPlayer(string name, bool isAI)
    {
        Name = name;
        IsAI = isAI;
    }

    public void SelectCard(int cardIndex)
    {
        if(ArrayHelper.IsValidIndex(cardIndex, Hand.Count))
        {
            _selectedCardIndex = cardIndex;
            Debug.Log("Selected Card: " + CurrentSelectedCard.Name);
        }
    }

    public void PlayAbilityCard(List<ITargetable> possibleTargets, int targetIndex)
    {
        AbilityCard targetCard = Hand.GetCard(targetIndex);
        //TODO figure out how to get possible targets
        //List<ITargetable> possibleTargets = ??
        //targetCard.Play(possibleTargets, CurrentTarget);
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
                newCard.Draw(this);
                Debug.Log("New Card: " + newCard.Name);

                DrewCard.Invoke(newCard);
            }
        }
    }

    public void Target()
    {
        Debug.Log("Targeted: " + Name);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(Name + " took " + damage + " damage.");
    }

    public void Kill()
    {
        Debug.Log(Name + " has died!");
    }
}
