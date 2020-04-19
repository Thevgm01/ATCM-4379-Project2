using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectTargetPlayerState : CardGameState
{
    public static event Action Entered = delegate { };
    public static event Action Exited = delegate { };

    CardPlayer _player = null;

    InputController _input = null;
    TargetController _targetController = null;

    private void Start()
    {
        _player = StateMachine.PlayerController.CurrentPlayer;
        _input = StateMachine.Input;
        _targetController = StateMachine.TargetController;
    }

    public override void Enter()
    {
        Debug.Log("SELECT TARGET");
        // wait for player to click a target, or cancel
        _targetController.TargetClicked += OnTargetClicked;
        _input.PressedCancel += OnPressedCancel;

        Entered?.Invoke();
    }

    public override void Exit()
    {
        _targetController.TargetClicked -= OnTargetClicked;
        _input.PressedCancel -= OnPressedCancel;

        Exited?.Invoke();
    }

    void OnTargetClicked(ITargetable target)
    {
        _player.CurrentSelectedCard.Play(target);
        StateMachine.ChangeState<DecideNextPlayerState>();
    }

    void OnPressedCancel()
    {
        StateMachine.ChangeState<PlayerCardSelectState>();
    }
}
