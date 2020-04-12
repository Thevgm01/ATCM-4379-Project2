using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardGameSM))]
public class PlayerTurnStartState : CardGameState
{
    public override void Enter()
    {
        Debug.Log("Starting Player Turn");
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
        StateMachine.ChangeState<PlayerCardSelectState>();
    }

    void OnPressedCancel()
    {
        StateMachine.ChangeState<EnemyTurnStartState>();
    }
}
