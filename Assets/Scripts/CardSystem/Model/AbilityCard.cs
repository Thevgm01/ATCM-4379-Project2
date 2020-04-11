using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilityCard : Card
{
    public string Name { get; private set; }
    public int Cost { get; private set; }
    public string Description { get; private set; }
    public Attribute Attribute { get; private set; }
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
        Debug.Log("PLAY: " + Name);
    }

    public override void OnDiscard()
    {
        //
    }

    public override void OnDraw()
    {
        //
    }

    /*
    // consts
    const int MAX_HEALTH = 99;
    // statics
    // events
    public event Action Killed = delegate { };
    public event Action<int> Damaged = delegate { };
    // properties
    public int Health 
    {
        get => _health;
        private set
        {
            if (value <= 0)
            {
                value = Mathf.Clamp(value, 0, MAX_HEALTH);
            }
            _health = value;
        }
    }
    // private data
    int _health;
    */


    /*
    public void Kill()
    {
        // code related to creature dying here
        Killed.Invoke();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Damaged.Invoke(damage);

        if(Health <= 0)
        {
            Kill();
        }
    }
    */
}
