using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilityCard : Card
{
    public string Name { get; private set; }
    public int Cost { get; private set; }
    public string Description { get; private set; }
    public AbilityCardData.CardAttribute Attribute { get; private set; }
    public Sprite Graphic { get; private set; }

    // constructor, Awake, Start
    public AbilityCard(AbilityCardData data)
    {
        Name = data.Name;
        Cost = data.Cost;
        Description = data.Description;
        Attribute = data.Attribute;
        Graphic = data.Graphic;
    }
    // update, loops

    // public methods
    public override void Play()
    {
        base.Play();
        Debug.Log("PLAY: " + Name);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        //
    }

    public override void OnDraw()
    {
        base.OnDraw();
        //
    }
}
