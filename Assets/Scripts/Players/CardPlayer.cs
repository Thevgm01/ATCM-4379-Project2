using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CardPlayer : MonoBehaviour
{
    public Action<Card> DestroyCard = delegate { };

    public Deck<Card> draw, hand, discard, weaponsToFire, weaponsFired;
    List<Ship> fleet;

    [SerializeField] StartingDeck startingDeck;

    [SerializeField] Animator gameStateMachine;

    protected CardViewHandler cardViewer;

    public Collider field;

    void Awake()
    {
        draw = new Deck<Card>();
        hand = new Deck<Card>();
        discard = new Deck<Card>();
        weaponsToFire = new Deck<Card>();
        weaponsFired = new Deck<Card>();

        fleet = new List<Ship>();

        cardViewer = GetComponent<CardViewHandler>();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        draw.Emptied += ShuffleOnEmpty;

        if (cardViewer != null)
        {
            DestroyCard += cardViewer.DestroyCardView;

            draw.CardAdded += cardViewer.ReorganizeDraw;
            draw.CardRemoved += cardViewer.ReorganizeDraw;
            hand.CardAdded += cardViewer.ReorganizeHand;
            hand.CardRemoved += cardViewer.ReorganizeHand;
            discard.CardAdded += cardViewer.ReorganizeDiscard;
            discard.CardRemoved += cardViewer.ReorganizeDiscard;
            weaponsToFire.CardAdded += cardViewer.ReorganizeWeaponsFired;
            weaponsToFire.CardRemoved += cardViewer.ReorganizeWeaponsFired;
            weaponsFired.CardAdded += cardViewer.ReorganizeWeaponsFired;
            weaponsFired.CardRemoved += cardViewer.ReorganizeWeaponsFired;
        }

        //cardsView = new List<CardView>();

        foreach (CardData cardData in startingDeck.StartingCards)
        {
            if (cardData == null)
            {
                Debug.LogWarning("Missing card in starting deck: " + startingDeck.Name);
                continue;
            }
            Card card = CreateNewCard(cardData);
            cardViewer?.CreateNewCardView(card, cardData);
            hand.Add(card);
        }

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

        draw.Shuffle();
    }

    Card CreateNewCard(CardData cardData)
    {
        Card newCard = null;

        if (cardData is AbilityCardData abilityData) newCard = new AbilityCard(abilityData);
        else if (cardData is DefenseCardData defenseData) newCard = new DefenseCard(defenseData);
        else if (cardData is ShipCardData shipData) newCard = new ShipCard(shipData);
        else if (cardData is WeaponCardData weaponData) newCard = new WeaponCard(weaponData);
        else Debug.LogError("Unsupported CardData type.");

        return newCard;
    }

    void ShuffleOnEmpty()
    {
        discard.Shuffle();
        draw.MergeDeck(discard);
    }

    protected bool IsValidTarget(CardData.TargetType target, Transform transform)
    {
        Ship ship;
        Debug.Log("hello");

        switch (target)
        {
            case CardData.TargetType.AllAllyShips: return transform != null;
            case CardData.TargetType.AllEnemyShips: return transform != null;
            case CardData.TargetType.AllyField: return transform == field.transform;
            case CardData.TargetType.EnemyField: return transform != field.transform;
            case CardData.TargetType.AllyShip:
                ship = transform.GetComponent<Ship>();
                if (ship != null)
                    return ship.owner == this;
                return false;
            case CardData.TargetType.EnemyShip:
                ship = transform.GetComponent<Ship>();
                if (ship != null)
                    return ship.owner != this;
                return false;
            default:
                return false;
        }
    }

    protected void TryPlayCard(Card card, RaycastHit hit)
    {
        if (card.Data != null &&
            IsValidTarget(card.Data.Target, hit.transform))
        {
            if (card is ShipCard)
            {
                fleet.Add(((ShipCard)card).MakeShip(this, hit.point, field.transform));
                DestroyCard?.Invoke(card);
                GetDeck(card).Remove(card);
            }
            else if (card is WeaponCard)
            {
                if (hit.transform.GetComponent<Ship>().TryAddWeapon((WeaponCard)card))
                {
                    GetDeck(card).Remove(card);
                    weaponsFired.Add(card);
                }
            }
        }
    }

    protected Deck<Card> GetDeck(Card card)
    {
        if (draw.Contains(card)) return draw;
        if (hand.Contains(card)) return hand;
        if (discard.Contains(card)) return discard;
        if (weaponsToFire.Contains(card)) return weaponsToFire;
        if (weaponsFired.Contains(card)) return weaponsFired;
        return null;
    }
}
