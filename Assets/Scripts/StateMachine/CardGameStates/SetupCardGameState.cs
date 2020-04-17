using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardGameSM))]
public class SetupCardGameState : CardGameState
{
    InputController _input = null;
    CardGameDeckController _deckController = null;

    private void Start()
    {
        _input = StateMachine.Input;
        _deckController = StateMachine.DeckController;
    }

    public override void Enter()
    {
        Debug.Log("Card Setup State. Do fancy animations to build the board.");
        // subscribe to inputs
        _input.PressedConfirm += OnPressedConfirm;

        _deckController.AbilityCardDeck.Shuffle();
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
}
