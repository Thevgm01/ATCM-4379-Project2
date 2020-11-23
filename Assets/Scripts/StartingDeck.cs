using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Starting Deck", menuName = "Starting Deck")]
public class StartingDeck : ScriptableObject
{
    [SerializeField] string _name = "...";
    public string Name => _name;

    [SerializeField] CardData[] _startingCards = null;
    public CardData[] StartingCards => _startingCards;

    [SerializeField] CardData[] _cards = null;
    public CardData[] Cards => _cards;
}
