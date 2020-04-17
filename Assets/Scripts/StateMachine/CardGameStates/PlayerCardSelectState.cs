using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardGameSM))]
public class PlayerCardSelectState : CardGameState
{
    CardPlayer _player = null;
    int _currentSelectionIndex = 0;

    int PlayerHandSize => _player.Hand.Count;
    public AbilityCard SelectedCard => _player.Hand.GetCard(_currentSelectionIndex);

    public override void Enter()
    {
        _player = StateMachine.PlayerController.CurrentPlayer;

        Debug.Log("Player Card Select");
        StateMachine.Input.PressedRight += OnPressedRight;
        StateMachine.Input.PressedLeft += OnPressedLeft;
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
        StateMachine.Input.PressedCancel += OnPressedCancel;

        Debug.Log("Initial Selected Card: " + SelectedCard.Name);
    }

    public override void Exit()
    {
        StateMachine.Input.PressedRight -= OnPressedRight;
        StateMachine.Input.PressedLeft -= OnPressedLeft;
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
        StateMachine.Input.PressedCancel -= OnPressedCancel;
    }

    void OnPressedRight()
    {
        // move to next selection
        _currentSelectionIndex = ArrayHelper.GetNextLoopedIndex(_currentSelectionIndex, PlayerHandSize);
        Debug.Log("Selected Card: " + SelectedCard.Name);
    }

    void OnPressedLeft()
    {
        _currentSelectionIndex = ArrayHelper.GetPreviousLoopedIndex(_currentSelectionIndex, PlayerHandSize);
        Debug.Log("Selected Card: " + SelectedCard.Name);
    }

    void OnPressedConfirm()
    {
        SelectedCard.Play();    //TODO consider making this another state

        StateMachine.ChangeState<DecideNextPlayerState>();
    }

    void OnPressedCancel()
    {
        // Pass, for now
        StateMachine.ChangeState<DecideNextPlayerState>();
    }
}
