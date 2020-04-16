using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardGameSM))]
public class CardSetupState : CardGameState
{
    [Header("Game Settings")]
    [SerializeField] int _numberOfHumans = 1;
    [SerializeField] int _numberOfBots = 1;

    [Header("Deck Info")]
    [SerializeField] AbilityCardDeckConfig _abilityDeckData = null;

    CardGameDeckController _deckController = null;
    CardPlayerController _playerController = null;
    InputController _input = null;

    private void Start()
    {
        _deckController = StateMachine.DeckController;
        _playerController = StateMachine.PlayerController;
        _input = StateMachine.Input;
    }

    public override void Enter()
    {
        Debug.Log("Card Setup State");
        // subscribe to inputs
        _input.PressedConfirm += OnPressedConfirm;

        BuildAbilityDeck(_abilityDeckData);
        CreateHumanPlayers(_numberOfHumans);
        CreateBotPlayers(_numberOfBots);
    }

    public override void Exit()
    {
        // unsub from inputs
        _input.PressedConfirm -= OnPressedConfirm;
    }

    void OnPressedConfirm()
    {
        StateMachine.ChangeState<PlayerTurnStartState>();
    }

    void BuildAbilityDeck(AbilityCardDeckConfig abilityDeckData)
    {
        // each player should draw starting cards
        _deckController.CreateStartingDeck(_abilityDeckData);
    }

    private void CreateBotPlayers(int numberOfBots)
    {
        for (int i = 0; i < numberOfBots; i++)
        {
            _playerController.AddPlayer(true);
        }
    }

    private void CreateHumanPlayers(int numberOfHumans)
    {
        for (int i = 0; i < numberOfHumans; i++)
        {
            _playerController.AddPlayer(false);
        }
    }
}
