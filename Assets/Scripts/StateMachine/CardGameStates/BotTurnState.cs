using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotTurnState : CardGameState
{
    [Header("Enemy Turn Settings")]
    [SerializeField] float _botPauseDuration = 2f;

    CardPlayer _player;

    int _cardsToDraw;
    Coroutine _enemyPauseRoutine = null;
    CardGameDeckController _deckController = null;
    CardPlayerController _playerController = null;
    Deck<AbilityCard> _abilityCardDeck = null;

    private void Start()
    {
        _deckController = StateMachine.DeckController;
        _playerController = StateMachine.PlayerController;
        _abilityCardDeck = _deckController.AbilityCardDeck;

        _cardsToDraw = StateMachine.CardDrawPerTurn;
    }

    public override void Enter()
    {
        Debug.Log("Enemy Turn");
        _player = _playerController.CurrentPlayer;

        Debug.Log("Enemy draws 2 cards");
        int cardsToDraw = StateMachine.CardDrawPerTurn;
        _player.DrawAbilityCard(_cardsToDraw, _abilityCardDeck);

        Debug.Log("...Enemy Thinking...");
        if (_enemyPauseRoutine != null)
            StopCoroutine(_enemyPauseRoutine);
        _enemyPauseRoutine = StartCoroutine(EnemyPauseRoutine(_botPauseDuration));
    }

    public override void Exit()
    {
        // just in case
        if (_enemyPauseRoutine != null)
            StopCoroutine(_enemyPauseRoutine);
    }

    IEnumerator EnemyPauseRoutine(float pauseDuration)
    {
        // wait for thinking
        yield return new WaitForSeconds(pauseDuration);
        Debug.Log("Enemy Acted.");

        StateMachine.ChangeState<DecideNextPlayerState>();
    }
}
