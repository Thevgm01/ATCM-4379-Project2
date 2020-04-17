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
    Coroutine _playerStartRoutine = null;

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

        // start player turn setup animation
        if (_playerStartRoutine != null)
            StopCoroutine(_playerStartRoutine);
        _playerStartRoutine = StartCoroutine(TurnStartRoutine());
    }

    public override void Exit()
    {

    }

    IEnumerator TurnStartRoutine()
    {
        Debug.Log("Player draws 2 cards");
        _player.DrawAbilityCard(_cardsToDraw, _abilityCardDeck);

        yield return new WaitForSeconds(1.5f);

        Debug.Log("Player turn setup complete.");
        StateMachine.ChangeState<PlayerCardSelectState>();
    }
}
