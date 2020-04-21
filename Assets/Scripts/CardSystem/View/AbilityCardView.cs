using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ViewClickable))]
public class AbilityCardView : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] Text _nameTextUI = null;
    [SerializeField] Text _costTextUI = null;
    [SerializeField] Text _descriptionTextUI = null;
    [SerializeField] Image _graphicUI = null;
    [SerializeField] Image _backsideUI = null;
    [SerializeField] Image _frontBackgroundUI = null;

    DeckStyleData _deckStyleData = null;

    public AbilityCard CurrentCard { get; private set; }

    PlayerView _playerView = null;
    ViewClickable _viewClickable = null;

    private void Awake()
    {
        _viewClickable = GetComponent<ViewClickable>();
    }

    private void OnEnable()
    {
        _viewClickable.Clicked += OnClicked;
        _viewClickable.MouseEntered += OnMouseEntered;
        _viewClickable.MouseExited += OnMouseExited;
    }

    private void OnDisable()
    {
        _viewClickable.Clicked -= OnClicked;
        _viewClickable.MouseEntered -= OnMouseEntered;
        _viewClickable.MouseExited -= OnMouseExited;
    }

    public void Setup(PlayerView playerView, DeckStyleData deckStyle)
    {
        _playerView = playerView;
        _deckStyleData = deckStyle;
    }

    public void LoadNewCard(AbilityCard card)
    {
        CurrentCard = card;
        UpdateDisplay(card);
        // assign the target when the view is clicked
    }

    public void UpdateDisplay(AbilityCard card)
    {
        _nameTextUI.text = card.Name;
        _costTextUI.text = card.Cost.ToString();
        _descriptionTextUI.text = card.Description;
        // sprites
        _graphicUI.sprite = card.Graphic;
        // deck style
        if(_deckStyleData != null)
        {
            _backsideUI.sprite = _deckStyleData.CardBack;
            _frontBackgroundUI.sprite = _deckStyleData.CardFront;
        }

        DetermineCardFlip(card.Visibility);
    }

    void DetermineCardFlip(CardVisibility visibility)
    {
        switch (visibility)
        {
            // only show backside
            case CardVisibility.None:
                _backsideUI.gameObject.SetActive(true);
                break;
            // show the owning player
            case CardVisibility.Player:
                _backsideUI.gameObject.SetActive(false);
                break;
            // show everyone
            case CardVisibility.Everyone:
                _backsideUI.gameObject.SetActive(false);
                break;
            default:
                Debug.Log("Card visibility undefined...");
                break;
        }
    }

    public void SetClickable(bool isClickable)
    {
        _viewClickable.SetClickable(isClickable);
    }

    void OnClicked()
    {
        _playerView.SetTarget(CurrentCard);
    }

    void OnMouseEntered()
    {
        //TODO add hover effects
    }

    void OnMouseExited()
    {
        //TODO revert hover effects
    }
}
