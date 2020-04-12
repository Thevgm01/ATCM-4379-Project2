using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardGameSM))]
public class CardSetupState : CardGameState
{
    public override void Enter()
    {
        Debug.Log("Card Setup State");
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
    }

    public override void Exit()
    {
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
    }

    void OnPressedConfirm()
    {
        StateMachine.ChangeState<PlayerTurnStartState>();
    }
}
