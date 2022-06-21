using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireRevive : Upgrade
{
    public CampfireRevive()
    {
        cost = 2;
        buttonText = "Revive";
        descriptionText = "Revives and fullheals character. Costs " + cost + ".";
    }

    public override bool TryUpgrade(Creature creature, bool positive, bool unlimitedSpace)
    {
        creature.GetComponentInChildren<Health>().Heal();
        return true;
    }
}
