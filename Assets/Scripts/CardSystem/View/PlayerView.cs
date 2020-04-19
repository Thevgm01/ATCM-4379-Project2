using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    //TODO make this an Object Pool
    [SerializeField] AbilityCardView _cardViewPrefab = null;

    List<AbilityCardView> _cardViews = new List<AbilityCardView>();

    CardPlayer _player = null;
    TargetController _targetController = null;
    bool _hasInitialized = false;

    public void Setup(CardPlayer player, TargetController targetController)
    {
        _player = player;
        _targetController = targetController;
    }

    private void OnEnable()
    {
        // don't subscribe to events until next frame, when we're sure everything else has initialized
        if (!_hasInitialized) { return; }

        SubscribeToEvents();
    }

    private void Start()
    {
        // subscribe to events here, but after that use OnEnable/OnDisable
        if (!_hasInitialized)
        {
            _hasInitialized = true;
            SubscribeToEvents();
        }
    }

    void SubscribeToEvents()
    {
        _player.DrewCard += OnDrewCard;
        SelectTargetPlayerState.Entered += OnPlayerSelectEntered;
        SelectTargetPlayerState.Exited += OnPlayerSelectExited;
    }

    private void OnDisable()
    {
        _player.DrewCard -= OnDrewCard;
        SelectTargetPlayerState.Entered -= OnPlayerSelectEntered;
        SelectTargetPlayerState.Exited -= OnPlayerSelectExited;
    }

    public void AddNewCard(AbilityCard card)
    {
        AbilityCardView cardView = Instantiate(_cardViewPrefab, this.transform, false);
        _cardViews.Add(cardView);

        cardView.Setup(this);
        cardView.LoadNewCard(card);
    }

    public void SetTarget(ITargetable target)
    {
        Debug.Log("Set new target!");
        _targetController.ActivateNewTarget(target);
    }

    void OnDrewCard(AbilityCard newCard)
    {
        AddNewCard(newCard);
    }

    void OnPlayerSelectEntered()
    {
        foreach(AbilityCardView cardView in _cardViews)
        {
            // do further checking here if you like
            cardView.SetClickable(true);
        }
    }

    void OnPlayerSelectExited()
    {
        foreach (AbilityCardView cardView in _cardViews)
        {
            // do further checking here if you like
            cardView.SetClickable(false);
        }
    }
}
