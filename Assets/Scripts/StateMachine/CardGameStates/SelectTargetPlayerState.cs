using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTargetPlayerState : CardGameState
{
    CardPlayerController _playerController = null;
    CardPlayer _player = null;
    InputController _input = null;

    List<ITargetable> _targets = new List<ITargetable>();
    ITargetable CurrentTarget => _targets[_currentTargetIndex];

    AbilityCard _selectedCard = null;
    int _currentTargetIndex = 0;

    private void Start()
    {
        _playerController = StateMachine.PlayerController;
        _input = StateMachine.Input;
    }

    public override void Enter()
    {
        Debug.Log("SELECT TARGET");
        GetTargets();
        _player = _playerController.CurrentPlayer;
        // select targets with left/right
        _input.PressedLeft += OnPressedLeft;
        _input.PressedRight += OnPressedRight;
        _input.PressedConfirm += OnPressedConfirm;
        _input.PressedCancel += OnPressedCancel;
    }

    public override void Exit()
    {
        _input.PressedLeft -= OnPressedLeft;
        _input.PressedRight -= OnPressedRight;
        _input.PressedConfirm -= OnPressedConfirm;
        _input.PressedCancel -= OnPressedCancel;
    }

    void GetTargets()
    {
        //TODO optionally get targetable cards here, if you want cards to be targetable
        foreach(CardPlayer player in _playerController.Players)
        {
            // this only works if player is targetable
            _targets.Add(player);
        }
    }

    void OnPressedLeft()
    {
        _currentTargetIndex = ArrayHelper.GetPreviousLoopedIndex(_currentTargetIndex, _targets.Count);
        CurrentTarget.Target();
    }

    void OnPressedRight()
    {
        _currentTargetIndex = ArrayHelper.GetNextLoopedIndex(_currentTargetIndex, _targets.Count);
        CurrentTarget.Target();

    }

    void OnPressedConfirm()
    {
        _player.CurrentSelectedCard.Play(_player, CurrentTarget);
        StateMachine.ChangeState<DecideNextPlayerState>();
    }

    void OnPressedCancel()
    {
        StateMachine.ChangeState<PlayerCardSelectState>();
    }
}
