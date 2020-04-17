using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardGameSM))]
public class PlayerCardSelectState : CardGameState
{
    public override void Enter()
    {
        Debug.Log("Player Card Select");
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
        StateMachine.ChangeState<BotTurnState>();
    }

    void OnPressedCancel()
    {
        StateMachine.ChangeState<BotTurnState>();
    }
}
