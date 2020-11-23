using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardPlayer : MonoBehaviour
{
    public Deck<Card> draw, hand, discard, weaponQueue;
    List<Ship> fleet;

    [SerializeField] StartingDeck startingDeck;

    CardViewHandler cardViewer;

    void Awake()
    {
        draw = new Deck<Card>();
        hand = new Deck<Card>();
        discard = new Deck<Card>();
        weaponQueue = new Deck<Card>();

        fleet = new List<Ship>();

        cardViewer = GetComponent<CardViewHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        draw.Emptied += ShuffleOnEmpty;

        if (cardViewer != null)
        {
            draw.CardAdded += cardViewer.ReorganizeDraw;
            draw.CardRemoved += cardViewer.ReorganizeDraw;
            hand.CardAdded += cardViewer.ReorganizeHand;
            hand.CardRemoved += cardViewer.ReorganizeHand;
            discard.CardAdded += cardViewer.ReorganizeDiscard;
            discard.CardRemoved += cardViewer.ReorganizeDiscard;
            weaponQueue.CardAdded += cardViewer.ReorganizeWeaponQueue;
            weaponQueue.CardRemoved += cardViewer.ReorganizeWeaponQueue;
        }

        //cardsView = new List<CardView>();

        foreach (CardData cardData in startingDeck.Cards)
        {
            if(cardData == null)
            {
                Debug.LogWarning("Missing card in starting deck: " + startingDeck.Name);
                continue;
            }
            Card card = CreateNewCard(cardData);
            cardViewer?.CreateNewCardView(card, cardData);
            draw.Add(card);
        }
    }

    Card CreateNewCard(CardData cardData)
    {
        Card newCard = null;

        if (cardData is AbilityCardData) newCard = new AbilityCard((AbilityCardData)cardData);
        else if (cardData is DefenseCardData) newCard = new DefenseCard((DefenseCardData)cardData);
        else if (cardData is ShipCardData) newCard = new ShipCard((ShipCardData)cardData);
        else if (cardData is WeaponCardData) newCard = new WeaponCard((WeaponCardData)cardData);
        else Debug.LogError("Unsupported CardData type.");

        return newCard;
    }

    void ShuffleOnEmpty()
    {
        discard.Shuffle();
        draw.MergeDeck(discard);
    }
}
