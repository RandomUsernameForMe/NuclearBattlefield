using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BashUpgrade : UpgradeBuilder
{
    private int v;
    private int b;

    public BashUpgrade(int v, int b)
    {
        this.v = v;
        this.b = b;
        cost = 3;
        buttonText = "BASH HARDER";
        descriptionText = "Increase the power of your stun by " + v + " and the duration by " + b;
    }

    public override void Upgrade(Creature creature)
    {
        creature.GetComponentInChildren<ShieldBash>().bashStrength += v;
        creature.GetComponentInChildren<ShieldBash>().stunDuration += b;
        UpgradesManager.PayPoints(cost);
    }
}