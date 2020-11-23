﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card
{
    public CardData Data { get; protected set; } = null;

    public abstract void Play(Transform target);
}
