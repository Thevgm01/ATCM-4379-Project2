using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardViewHandler : MonoBehaviour
{
    public enum CardVisibility
    {
        Always,
        Default,
        Never
    }

    public enum CardStacking
    {
        Ascending,
        Centered
    }

    public CardPlayer cardPlayer;

    public CardVisibility cardVisibilityOverride;

    [SerializeField] Transform drawPile, handPile, discardPile, weaponQueuePile;
    [SerializeField] GameObject cardPrefab;

    [SerializeField] [Range(0f, 10f)] float lerpSpeed;

    Dictionary<Card, CardView> cardsViewDictionary;
    Dictionary<CardView, Card> cardsViewDictionary_inverse;

    CardView hoveredCard;

    void Awake()
    {
        cardsViewDictionary = new Dictionary<Card, CardView>();
        cardsViewDictionary_inverse = new Dictionary<CardView, Card>();
    }

    public void CreateNewCardView(Card card, CardData cardData)
    {
        var newCardGameObject = Instantiate(cardPrefab);
        newCardGameObject.name = cardData.Name;

        var cardView = newCardGameObject.GetComponent<CardView>();
        cardView.LoadCardData(cardData);
        //if (cardVisibilityOverride == CardVisibility.Always) cardView.SetVisible(true);
        //else if (cardVisibilityOverride == CardVisibility.Never) cardView.SetVisible(false);

        cardView.MouseOver += CardHovered;
        cardsViewDictionary.Add(card, cardView);
        cardsViewDictionary_inverse.Add(cardView, card);
    }

    public void ReorganizeDraw(Card newCard)
    {
        ReorganizeDeck(newCard, cardPlayer.draw, drawPile, new Vector3(0, 0, -0.1f), CardStacking.Ascending, false);
    }

    public void ReorganizeHand(Card newCard)
    {
        ReorganizeDeck(newCard, cardPlayer.hand, handPile, new Vector3(3f, 0, -0.1f), CardStacking.Centered, true);
    }

    public void ReorganizeDiscard(Card newCard)
    {
        ReorganizeDeck(newCard, cardPlayer.discard, discardPile, new Vector3(0, 0, -0.1f), CardStacking.Ascending, true);
    }

    public void ReorganizeWeaponQueue(Card newCard)
    {
        ReorganizeDeck(newCard, cardPlayer.weaponQueue, weaponQueuePile, new Vector3(0, 1f, -0.1f), CardStacking.Centered, true);
    }

    private void ReorganizeDeck(Card newCard, Deck<Card> deck, Transform pile, Vector3 offset, CardStacking stacking, bool visibility)
    {
        if (newCard != null && deck.Contains(newCard))
            cardsViewDictionary[newCard].transform.parent = pile;

        if (stacking == CardStacking.Centered && deck.Count > 4)
        {
            offset.x = 4 * offset.x / deck.Count;
        }

        int i = 0;
        foreach (Card card in deck)
        {
            CardView cardView = cardsViewDictionary[card];

            if (stacking == CardStacking.Ascending) cardView.SetPosition(offset * i);
            else if (stacking == CardStacking.Centered) cardView.SetPosition(offset * (i - deck.Count * 0.5f + 0.5f));

            if (cardVisibilityOverride == CardVisibility.Always) cardView.SetVisible(true);
            else if (cardVisibilityOverride == CardVisibility.Never) cardView.SetVisible(false);
            else if (cardVisibilityOverride == CardVisibility.Default) cardView.SetVisible(visibility);

            ++i;
        }
    }

    void Update()
    {
        float lerpAmount = lerpSpeed * Time.deltaTime;

        foreach (CardView cardView in cardsViewDictionary.Values)
        {
            cardView.Move(lerpAmount);
        }
    }

    void CardHovered(CardView cardView)
    {
        if (cardView == hoveredCard) return;
        if (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0) return;

        Card card = cardsViewDictionary_inverse[cardView];
        if (cardPlayer.hand.Contains(card))
        {
            hoveredCard = cardView;
            ReorganizeHand(null);
            hoveredCard.SetPosition(hoveredCard.desiredPosition + new Vector3(0, 2, -2));
        }
        else
        {
            ReorganizeHand(null);
        }
    }
}
