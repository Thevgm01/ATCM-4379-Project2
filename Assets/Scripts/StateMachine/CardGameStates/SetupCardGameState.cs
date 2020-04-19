using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardGameSM))]
public class SetupCardGameState : CardGameState
{
    InputController _input = null;
    CardGameDeckController _deckController = null;

    Coroutine _setupAnimation = null;

    private void Start()
    {
        _input = StateMachine.Input;
        _deckController = StateMachine.DeckController;
    }

    public override void Enter()
    {
        Debug.Log("Card Setup State. Do fancy animations to build the board.");
        // start setup animation sequence
        if (_setupAnimation != null)
            StopCoroutine(_setupAnimation);
        _setupAnimation = StartCoroutine(SetupAnimation());
    }

    public override void Exit()
    {
        // unsub from inputs
        _input.PressedConfirm -= OnPressedConfirm;
    }

    IEnumerator SetupAnimation()
    {
        Debug.Log("Starting Setup Animation");

        Debug.Log("...Shuffling...");
        _deckController.AbilityCardDeck.Shuffle();
        yield return new WaitForSeconds(1.5f);

        Debug.Log("Waiting for Confirm input");
        _input.PressedConfirm += OnPressedConfirm;
    }

    void OnPressedConfirm()
    {
        if (_setupAnimation != null)
            StopCoroutine(_setupAnimation);

        StateMachine.ChangeState<PlayerTurnStartState>();
    }
}
