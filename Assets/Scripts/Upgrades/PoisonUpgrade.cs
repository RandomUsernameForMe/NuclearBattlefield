using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonUpgrade : GenericUpgrade
{
    private int v;

    public PoisonUpgrade(int v)
    {
        this.v = v;
        cost = 3;
        buttonText = "Deadlier poison.";
        descriptionText = "Add both damage and duration of your poison. Costs " + cost + " to upgrade.";
    }

    public override void Upgrade(Creature creature)
    {
        creature.GetComponentInChildren<PoisonBlast>().value += v;
        creature.GetComponentInChildren<PoisonBlast>().duration += v;
        creature.GetComponentInChildren<PoisonAmplifier>().power += v;
        UpgradesManager.PayPoints(cost);
    }
}
