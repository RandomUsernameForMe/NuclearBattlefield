using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackUpgrade : GenericUpgrade
{
    private int v;

    public BasicAttackUpgrade(int v)
    {
        this.v = v;
        cost = 2;
        buttonText = "Work out.";
        descriptionText = "Do " + v + " more physical damage with your strike. Costs " + cost + " to upgrade.";
    }

    public override void Upgrade(Creature creature)
    {
        creature.GetComponentInChildren<PhysicalWeapon>().value += v;
        UpgradesManager.PayPoints(cost);
    }
}
