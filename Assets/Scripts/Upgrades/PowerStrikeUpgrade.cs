using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStrikeUpgrade : GenericUpgrade
{
    private int v;
    public PowerStrikeUpgrade(int v)
    {
        this.v = v;
        cost = 4;
        buttonText = "Equip Destroyer!";
        descriptionText = "Just devastate. Costs " + cost + " to upgrade.";
    }

    public override void Upgrade(Creature creature)
    {
        GameObject.Destroy(creature.GetComponentInChildren<MightyWeapon>());
        creature.gameObject.AddComponent(typeof(Destroyer));
        UpgradesManager.PayPoints(cost);
    }
}
