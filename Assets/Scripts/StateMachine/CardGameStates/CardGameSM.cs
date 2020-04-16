using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameSM : StateMachine
{
    public int Rounds => _rounds;
    // referenced properties
    public InputController Input => _input;
    public CardGameDeckController DeckController { get; private set; } = new CardGameDeckController();
    public CardPlayerController PlayerController { get; private set; } = new CardPlayerController();

    [Header("References")]
    [SerializeField] InputController _input = null;
    
    [Header("Game Settings")]
    [SerializeField] int _rounds = 3;

    private void OnEnable()
    {
        DeckController.SubscribeToEvents();
    }

    private void OnDisable()
    {
        DeckController.UnsubscribeFromEvents();
    }

    private void Start()
    {
        ChangeState<CardSetupState>();
    }
}
