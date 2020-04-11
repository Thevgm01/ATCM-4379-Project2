using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTester : MonoBehaviour
{
    [SerializeField] List<AbilityCardData> _abilityDeckData = new List<AbilityCardData>();
    [SerializeField] List<AbilityCardData> _playerStartingHandData = new List<AbilityCardData>();
    [SerializeField] List<AbilityCardData> _enemyStartingHandData = new List<AbilityCardData>();

    [SerializeField] AbilityCardView _actionCardView = null;

    Deck<AbilityCard> _abilityCardDeck = new Deck<AbilityCard>();
    Deck<AbilityCard> _abilityCardDeckDiscarded = new Deck<AbilityCard>();
    Deck<AbilityCard> _playerHand = new Deck<AbilityCard>();
    Deck<AbilityCard> _enemyHand = new Deck<AbilityCard>();

    private void Awake()
    {
        // create our decks
        _abilityCardDeck = DeckFactory.CreateDeck(_abilityDeckData);
        _playerHand = DeckFactory.CreateDeck(_playerStartingHandData);
        _enemyHand = DeckFactory.CreateDeck(_enemyStartingHandData);
    }

    private void Update()
    {
        // play all of player's cards
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PrintCards(_playerHand);
        }
        // player draws next card
        if (Input.GetKeyDown(KeyCode.W))
        {
            AbilityCard newCard = _abilityCardDeck.Draw(DeckPosition.Top);
            if(newCard != null)
            {
                Debug.Log("Drew card: " + newCard.Name);
                _playerHand.Add(newCard);
            }
        }
        // player draws next card
        if (Input.GetKeyDown(KeyCode.E))
        {
            AbilityCard newCard = _abilityCardDeck.Draw(DeckPosition.Middle);
            if (newCard != null)
            {
                Debug.Log("Drew card: " + newCard.Name);
                _playerHand.Add(newCard);
            }
        }
        // player all of enemy's cards
        if (Input.GetKeyDown(KeyCode.A))
        {
            PrintCards(_enemyHand);
        }
        // add a card to the enemy hand
        if (Input.GetKeyDown(KeyCode.S))
        {
            AbilityCard newCard = _abilityCardDeck.Draw(DeckPosition.Top);
            if(newCard != null)
            {
                Debug.Log("Drew card: " + newCard.Name);
                _enemyHand.Add(newCard);
            }
        }
        // shuffle player hand
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Shuffle");
            _playerHand.Shuffle();
        }
        // display card
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Display Player top card");
            Display(_playerHand.View(0));
        }
    }

    public void PrintCards(Deck<AbilityCard> hand)
    {
        for (int i = 0; i < hand.NumberOfItems; i++)
        {
            hand.View(i).Play();
        }
    }

    public void Display(AbilityCard card)
    {
        _actionCardView.Display(card);
    }
}
