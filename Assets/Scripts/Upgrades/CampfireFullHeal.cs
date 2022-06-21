using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireFullHeal : Upgrade
{
    public CampfireFullHeal()
    {
        cost = 1;
        buttonText = "Heal";
        descriptionText = "Heals character. Costs " + cost + ".";
    }

    public override bool TryUpgrade(Creature creature, bool positive,bool unlimitedSpace)
    {
        creature.GetComponentInChildren<Health>().Heal();
        return true;
    }
}