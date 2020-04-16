using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameDeckController
{
    // decks
    public Deck<AbilityCard> AbilityCardDeck { get; private set; } = new Deck<AbilityCard>();
    public Deck<AbilityCard> AbilityDiscardDeck { get; private set; } = new Deck<AbilityCard>();

    public void SubscribeToEvents()
    {
        AbilityCardDeck.Emptied += OnDiscardEmptied;
    }

    public void UnsubscribeFromEvents()
    {
        AbilityCardDeck.Emptied -= OnDiscardEmptied;
    }

    public void CreateStartingDeck(AbilityCardDeckConfig deckConfig)
    {
        AbilityCardDeck = DeckFactory.CreateDeck(deckConfig.Cards);
    }

    void OnDiscardEmptied()
    {
        Debug.Log("Out of cards! Reshuffling discard into main deck.");
        AbilityDiscardDeck.TransferDeckCards(AbilityCardDeck);
        AbilityCardDeck.Shuffle();
    }


}
