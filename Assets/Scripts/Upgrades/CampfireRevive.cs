using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireRevive : GenericUpgrade
{
    private int v;

    public CampfireRevive()
    {
        cost = 2;
        buttonText = "Revive";
        descriptionText = "Revives and fullheals character. Costs " + cost + ".";
    }

    public override void Upgrade(Creature creature)
    {
        creature.GetComponentInChildren<Health>().Heal();
        UpgradesManager.PayPoints(cost);
    }
}
