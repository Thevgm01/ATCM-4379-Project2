using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameDeckController
{
    // decks
    public Deck<AbilityCard> AbilityCardDeck { get; private set; } = new Deck<AbilityCard>();
    public Deck<AbilityCard> AbilityDiscardDeck { get; private set; } = new Deck<AbilityCard>();

    public void UnsubscribeFromEvents()
    {
        AbilityCardDeck.Emptied -= OnAbilityDeckEmptied;
    }

    public void CreateStartingDeck(AbilityCardDeckConfig deckConfig)
    {
        AbilityCardDeck = DeckFactory.CreateDeck(deckConfig.Cards);

        AbilityCardDeck.Emptied += OnAbilityDeckEmptied;
    }

    void OnAbilityDeckEmptied()
    {
        Debug.Log("EMPTY - Out of cards! Reshuffling discard into main deck.");
        AbilityDiscardDeck.TransferDeckCards(AbilityCardDeck);
        AbilityCardDeck.Shuffle();
    }
}
