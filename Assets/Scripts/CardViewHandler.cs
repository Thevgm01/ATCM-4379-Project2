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

    public Transform drawPile, handPile, discardPile, weaponsToFirePile, weaponsFiredPile;
    CardView cardPiles;
    float defaultHeight;
    [SerializeField] GameObject cardPrefab;

    [SerializeField] [Range(0f, 10f)] float lerpSpeed;

    public Dictionary<Card, CardView> cardsViewDictionary;
    public Dictionary<CardView, Card> cardsViewDictionary_inverse;

    public GameObject destroyCardParticles;

    void Awake()
    {
        cardPiles = drawPile.parent.gameObject.AddComponent<CardView>();
        defaultHeight = cardPiles.transform.localPosition.y;

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

        cardsViewDictionary.Add(card, cardView);
        cardsViewDictionary_inverse.Add(cardView, card);
    }

    public void ReorganizeDraw(Card newCard = null)
    {
        ReorganizeDeck(newCard, cardPlayer.draw, drawPile, new Vector3(0, 0, -0.1f), CardStacking.Ascending, false);
    }

    public void ReorganizeHand(Card newCard = null)
    {
        ReorganizeDeck(newCard, cardPlayer.hand, handPile, new Vector3(3f, 0, -0.1f), CardStacking.Centered, true);
    }

    public void ReorganizeDiscard(Card newCard = null)
    {
        ReorganizeDeck(newCard, cardPlayer.discard, discardPile, new Vector3(0, 0, -0.1f), CardStacking.Ascending, true);
    }

    private void ReorganizeDeck(Card newCard, Deck<Card> deck, Transform pile, Vector3 offset, CardStacking stacking, bool visibility)
    {
        if (newCard != null)
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

        cardPiles.Move(lerpAmount);

        foreach (CardView cardView in cardsViewDictionary.Values)
        {
            if (cardView == null) continue;
            cardView.Move(lerpAmount);
        }
    }

    public void MoveCardsOutOfTheWay()
    {
        cardPiles.SetPosition(new Vector3(0, defaultHeight - 4, 0));
    }

    public void MoveCardsBack()
    {
        cardPiles.SetPosition(new Vector3(0, defaultHeight, 0));
    }

    public void DestroyCardView(Card card)
    {
        CardView cardView = cardsViewDictionary[card];
        //cardsViewDictionary.Remove(card);
        //cardsViewDictionary_inverse.Remove(cardView);
        //Instantiate(destroyCardParticles, cardView.transform.position, cardView.transform.rotation);
        Destroy(cardView.gameObject);
    }
}
