using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingUpgrade : GenericUpgrade
{
    private int v;
    public HealingUpgrade(int v)
    {
        this.v = v;
        cost = 3;
        buttonText = "Better Healing";
        descriptionText = "Increase the power of your healing spell by " + v + "\n Costs " + cost + " to upgrade.";
    }

    public override void Upgrade(Creature creature)
    {
        creature.GetComponentInChildren<HealingWave>().value += v;
    }
}
