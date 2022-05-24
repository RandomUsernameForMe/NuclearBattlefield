using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireFullHeal : Upgrade
{
    private int v;

    public CampfireFullHeal()
    {
        cost = 1;
        buttonText = "Heal";
        descriptionText = "Heals character. Costs " + cost + ".";
    }

    public override bool TryUpgrade(Creature creature, bool positive)
    {
        creature.GetComponentInChildren<Health>().Heal();
        UpgradesManager.PayPoints(cost);
        return true;
    }
}