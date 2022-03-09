using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireFullHeal : UpgradeBuilder
{
    private int v;

    public CampfireFullHeal()
    {
        cost = 1;
        buttonText = "Heal";
        descriptionText = "Heals character. Costs " + cost + ".";
    }

    public override void Upgrade(Creature creature)
    {
        creature.GetComponentInChildren<Health>().Heal();
        UpgradesManager.PayPoints(cost);
    }
}