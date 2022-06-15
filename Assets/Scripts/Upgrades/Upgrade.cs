using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : IComparable
{
    public string buttonText;
    public string descriptionText;
    public int cost;

    public int CompareTo(object obj)
    {
        return buttonText.ToString().CompareTo(obj.ToString());
    }

    abstract public bool TryUpgrade(Creature creature, bool positive);
}