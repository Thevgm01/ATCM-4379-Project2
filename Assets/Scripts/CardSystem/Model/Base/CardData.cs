using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : ScriptableObject
{
    [SerializeField] string _name = "...";
    public string Name { get => _name; }

    [SerializeField] Sprite _graphic = null;
    public Sprite Graphic => _graphic;
}
