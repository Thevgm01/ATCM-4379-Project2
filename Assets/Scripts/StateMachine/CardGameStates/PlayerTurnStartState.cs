using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardGameSM))]
public class PlayerTurnStartState : CardGameState
{
    int _cardsToDraw;

    CardGameDeckController _deckController = null;
    CardPlayerController _playerController = null;
    Deck<AbilityCard> _abilityCardDeck = null;

    CardPlayer _player;

    private void Start()
    {
        _deckController = StateMachine.DeckController;
        _playerController = StateMachine.PlayerController;
        _abilityCardDeck = _deckController.AbilityCardDeck;

        _cardsToDraw = StateMachine.CardDrawPerTurn;
    }

    public override void Enter()
    {
        Debug.Log("Starting Player Turn");
        _player = _playerController.CurrentPlayer;

        Debug.Log("Player draws 2 cards");
        _player.DrawAbilityCard(_cardsToDraw, _abilityCardDeck);

        // listen for confirmation
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
        StateMachine.Input.PressedCancel += OnPressedCancel;
    }

    public override void Exit()
    {
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
        StateMachine.Input.PressedCancel -= OnPressedCancel;
    }

    void OnPressedConfirm() 
    {
        // do the same thing, whether they pressed confirm or cancel
        StateMachine.ChangeState<PlayerCardSelectState>();
    }

    void OnPressedCancel()
    {
        StateMachine.ChangeState<PlayerCardSelectState>();
    }
}
