using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericUpgrade
{
    public string buttonText;
    public string descriptionText;
    public int cost;

    abstract public void Upgrade(Creature creature);

}