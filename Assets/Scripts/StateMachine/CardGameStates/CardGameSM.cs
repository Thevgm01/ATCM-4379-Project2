using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameSM : StateMachine
{
    [Header("References")]
    [SerializeField] InputController _input;
    public InputController Input => _input;

    [Header("Game Settings")]
    [SerializeField] int _rounds = 3;
    public int Rounds => _rounds;

    [Header("Card Data")]
    [SerializeField] List<AbilityCardData> _abilityDeckData = new List<AbilityCardData>();
    [SerializeField] List<AbilityCardData> _playerStartingHandData = new List<AbilityCardData>();
    [SerializeField] List<AbilityCardData> _enemyStartingHandData = new List<AbilityCardData>();

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

    private void Start()
    {
        ChangeState<CardSetupState>();
    }
}
