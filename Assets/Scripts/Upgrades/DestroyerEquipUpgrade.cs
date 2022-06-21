using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerEquipUpgrade : Upgrade
{
    public DestroyerEquipUpgrade()
    {
        cost = 4;
        buttonText = "Equip Destroyer!";
        descriptionText = "Just devastate. Costs " + cost + " to upgrade.";
    }

    public override bool TryUpgrade(Creature creature, bool positive, bool unlimitedSpace)
    {
        GameObject.Destroy(creature.GetComponentInChildren<PowerStrike>());
        creature.gameObject.AddComponent<Destroyer>();
        return true;
    }
}
