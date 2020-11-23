using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCard : ComponentCard
{
    public WeaponCardData Data { get; private set; }

    public WeaponCard(WeaponCardData cardData)
    {
        Data = cardData;
    }

    public override void Play()
    {
        throw new System.NotImplementedException();
    }
}
