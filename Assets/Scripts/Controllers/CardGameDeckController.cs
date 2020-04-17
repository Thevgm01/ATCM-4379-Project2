using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameDeckController
{
    // decks
    public Deck<AbilityCard> AbilityCardDeck { get; private set; }
    public Deck<AbilityCard> AbilityDiscardDeck { get; private set; }

    public CardGameDeckController()
    {
        AbilityCardDeck = new Deck<AbilityCard>();
        AbilityDiscardDeck = new Deck<AbilityCard>();
    }

    public void UnsubscribeFromEvents()
    {
        AbilityCardDeck.Emptied -= OnAbilityDeckEmptied;
    }

    public void CreateStartingDeck(AbilityCardDeckConfig deckConfig)
    {
        Debug.Log("Building starting Ability deck");
        AbilityCardDeck = DeckFactory.CreateDeck(deckConfig.Cards);
        Debug.Log("Ability Deck count after build: " + AbilityCardDeck.Count);
        AbilityCardDeck.Emptied += OnAbilityDeckEmptied;
    }

    void OnAbilityDeckEmptied()
    {
        Debug.Log("EMPTY - Out of cards! Reshuffling discard into main deck.");
        AbilityDiscardDeck.TransferDeckCards(AbilityCardDeck);
        AbilityCardDeck.Shuffle();
    }
}
