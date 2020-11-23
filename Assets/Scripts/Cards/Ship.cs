using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship
{
    public CardPlayer owner { get; private set; }
    Deck<Card> weapons, defenses;

    int maxHitPoints;
    int hitPoints;

    float baseEvasionChance;
    float evasionChance;

    int maxComponentSlots;
    int componentSlots;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
