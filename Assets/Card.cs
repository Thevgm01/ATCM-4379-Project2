using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public string name;

    public Card(string n)
    {
        name = n;
    }

    public void Play()
    {
        Debug.Log("Playing card " + name);
    }
}
