﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CardPlayer : MonoBehaviour
{
    public Action<Card> DestroyCard = delegate { };

    public Deck<Card> draw, hand, discard, weapons;
    public List<Ship> fleet;

    [SerializeField] StartingDeck startingDeck;

    [SerializeField] Animator gameStateMachine;

    protected CardViewHandler cardViewer;

    public Collider field;

    protected int energy = 0;

    public AudioClip installModuleSound, warpInSound, errorSound;

    void Awake()
    {
        draw = new Deck<Card>();
        hand = new Deck<Card>();
        discard = new Deck<Card>();
        weapons = new Deck<Card>();

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

    public Ship RandomShip()
    {
        foreach (Ship s in fleet) if (s == null) fleet.Remove(s);

        if (fleet.Count == 0) return null;
        return fleet[(int)UnityEngine.Random.Range(0, fleet.Count)];
    }

    public void BeginTurn()
    {
        hand.Add(draw.Draw());
        energy += fleet.Count;
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

    protected bool IsValidTarget(CardData.TargetType target, Transform t)
    {
        Ship ship;

        switch (target)
        {
            case CardData.TargetType.AllAllyShips: return t != null;
            case CardData.TargetType.AllEnemyShips: return t != null;
            case CardData.TargetType.AllyField: return t == field.transform;
            case CardData.TargetType.EnemyField: return t != field.transform;
            case CardData.TargetType.AllyShip:
                ship = t.GetComponent<Ship>();
                return ship != null && ship.owner == this;
            case CardData.TargetType.EnemyShip:
                ship = t.GetComponent<Ship>();
                return ship != null && ship.owner != this;
            default:
                return false;
        }
    }

    protected bool TryPlayCard(Card card, Transform hit, Vector3 pos, bool playErrorSound = true)
    {
        if (card.Data != null &&
            IsValidTarget(card.Data.Target, hit.transform))
        {
            if (card is ShipCard)
            {
                AudioHelper.PlayClip2D(warpInSound, 1);
                fleet.Add(((ShipCard)card).MakeShip(this, pos, field.transform));
                DestroyCard?.Invoke(card);
                GetDeck(card).Remove(card);
                return true;
            }
            else if (card is WeaponCard)
            {
                WeaponCard weapon = (WeaponCard)card;
                Ship ship = hit.transform.GetComponent<Ship>();
                if (ship.TryAddWeapon(weapon))
                {
                    AudioHelper.PlayClip2D(installModuleSound, 1);
                    weapon.installedShip = ship;
                    weapons.Add(hand.Remove(card));
                    DestroyCard?.Invoke(card);
                    return true;
                }
                else
                {
                    if (playErrorSound) AudioHelper.PlayClip2D(errorSound, 1);
                    return false;
                }
            }
            else if (card is AbilityCard)
            {
                if (weapons.Count == 0)
                {
                    if (playErrorSound) AudioHelper.PlayClip2D(errorSound, 1);
                    return false;
                }

                WeaponCard weapon = null;
                while(weapon == null || weapon.installedShip == null)
                {
                    weapon = (WeaponCard)weapons.Draw();
                    weapons.Add(weapon, Deck<Card>.Position.Bottom);
                }
                WeaponCardData weaponData = (WeaponCardData)weapon.Data;

                if (energy >= weaponData.CostToFire)
                {
                    energy -= weaponData.CostToFire;
                    weapon.AttackShip(hit.transform.GetComponent<Ship>());
                    //weapons.Add(weapons.Draw(), Deck<Card>.Position.Bottom);
                    return true;
                }
                else
                {
                    if (playErrorSound) AudioHelper.PlayClip2D(errorSound, 1);
                    return false;
                }
            }
        }
        return false;
    }

    protected Deck<Card> GetDeck(Card card)
    {
        if (draw.Contains(card)) return draw;
        if (hand.Contains(card)) return hand;
        if (discard.Contains(card)) return discard;
        return null;
    }
}
