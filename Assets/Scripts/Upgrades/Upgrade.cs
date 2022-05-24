using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade
{
    public string buttonText;
    public string descriptionText;
    public int cost;

    abstract public bool TryUpgrade(Creature creature, bool positive);
}