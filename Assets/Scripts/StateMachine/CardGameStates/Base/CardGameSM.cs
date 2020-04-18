using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main State Machine controller for all of the game states in the Card Game.
/// This script should be responsible for any states, as well as any setup for classes
/// and references that are crucial to the states.
/// </summary>
public class CardGameSM : StateMachine
{
    public int Rounds => _rounds;
    // referenced properties
    public InputController Input => _input;
    public CardGameDeckController DeckController { get; private set; } = new CardGameDeckController();
    public CardPlayerController PlayerController { get; private set; } = new CardPlayerController();
    public TargetController TargetController { get; private set; } = new TargetController();

    public int CardDrawPerTurn => _cardDrawPerTurn;

    [Header("References")]
    [SerializeField] InputController _input = null;
    [SerializeField] AbilityCardDeckConfig _abilityDeckData = null;

    [Header("Game Settings")]
    [SerializeField] int _rounds = 3;
    [SerializeField] int _cardDrawPerTurn = 2;
    [SerializeField] int _numberOfHumans = 1;
    [SerializeField] int _numberOfBots = 1;

    private void OnDisable()
    {
        DeckController.UnsubscribeFromEvents();
    }

    private void Awake()
    {
        BuildAbilityDeck(_abilityDeckData);
        CreateHumanPlayers(_numberOfHumans);
        CreateBotPlayers(_numberOfBots);
    }

    private void Start()
    {
        ChangeState<SetupCardGameState>();
    }

    void BuildAbilityDeck(AbilityCardDeckConfig abilityDeckData)
    {
        // each player should draw starting cards
        DeckController.CreateStartingDeck(_abilityDeckData);
    }

    private void CreateBotPlayers(int numberOfBots)
    {
        for (int i = 0; i < numberOfBots; i++)
        {
            string name = "Bot" + (i+1).ToString();
            PlayerController.AddPlayer(name,true);
        }
    }

    private void CreateHumanPlayers(int numberOfHumans)
    {
        for (int i = 0; i < numberOfHumans; i++)
        {
            string name = "Human" + (i + 1).ToString();
            PlayerController.AddPlayer(name,false);
        }
    }
}
