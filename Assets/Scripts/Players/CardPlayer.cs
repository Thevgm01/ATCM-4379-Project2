using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardPlayer : MonoBehaviour
{
    Deck<Card> draw, hand, discard, weaponQueue;
    List<Ship> fleet;

    List<CardView> cardsView;
    Dictionary<Card, CardView> cardsViewDictionary;

    [SerializeField] StartingDeck startingDeck;
    [SerializeField] Transform drawPile, handPile, discardPile;
    [SerializeField] GameObject cardGameObject;

    // Start is called before the first frame update
    void Start()
    {
        draw = new Deck<Card>();
        hand = new Deck<Card>();
        discard = new Deck<Card>();
        weaponQueue = new Deck<Card>();

        draw.Emptied += ShuffleOnEmpty;

        fleet = new List<Ship>();
        cardsView = new List<CardView>();
        cardsViewDictionary = new Dictionary<Card, CardView>();

        foreach (CardData cardData in startingDeck.Cards)
        {
            CreateNewCard(cardData);
        }
    }

    void CreateNewCard(CardData cardData)
    {
        Card newCard = null;
        if (cardData is AbilityCardData) newCard = new AbilityCard((AbilityCardData)cardData);
        else if (cardData is DefenseCardData) newCard = new DefenseCard((DefenseCardData)cardData);
        else if (cardData is ShipCardData) newCard = new ShipCard((ShipCardData)cardData);
        else if (cardData is WeaponCardData) newCard = new WeaponCard((WeaponCardData)cardData);
        else Debug.LogError("Unsupported CardData type.");

        draw.Add(newCard);

        var newCardGameObject = Instantiate(cardGameObject);
        var cardView = newCardGameObject.GetComponent<CardView>();
        cardView.LoadCardData(cardData);
        cardsView.Add(cardView);
    }

    void ShuffleOnEmpty()
    {
        discard.Shuffle();
        draw.MergeDeck(discard);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
