using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityDeckConfig", menuName = "Card/DeckConfig/AbilityDeck")]
public class AbilityCardDeckConfig : ScriptableObject
{
    [SerializeField] List<AbilityCardData> _abilityData = new List<AbilityCardData>();
    public List<AbilityCardData> Cards => _abilityData;
}
