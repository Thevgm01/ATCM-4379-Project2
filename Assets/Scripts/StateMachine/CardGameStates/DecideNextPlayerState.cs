using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecideNextPlayerState : CardGameState
{
    CardPlayerController _playerController = null;
    CardPlayer _currentPlayer = null;

    private void Start()
    {
        _playerController = StateMachine.PlayerController;
    }

    public override void Enter()
    {
        // progress to next player
        _playerController.MakeNextPlayerCurrent();
        _currentPlayer = _playerController.CurrentPlayer;
    }

    public override void Tick()
    {
        // decide which state to progress to
        if (_currentPlayer.IsAI)
        {
            StateMachine.ChangeState<BotTurnState>();
        }
        else
        {
            StateMachine.ChangeState<PlayerTurnStartState>();
        }
    }

    public override void Exit()
    {
        
    }
}
