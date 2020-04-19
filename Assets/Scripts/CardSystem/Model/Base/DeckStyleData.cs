using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDeckStyle", menuName = "Card/DeckStyles")]
public class DeckStyleData : ScriptableObject
{
    [SerializeField] Sprite _cardFront = null;
    public Sprite CardFront => _cardFront;

    [SerializeField] Sprite _cardBack = null;
    public Sprite CardBack => _cardBack;
}
