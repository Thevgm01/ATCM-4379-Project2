using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Starting Deck", menuName = "Starting Deck")]
public class StartingDeck : ScriptableObject
{
    [SerializeField] CardData[] _cards = null;
    public CardData[] Cards => _cards;
}
