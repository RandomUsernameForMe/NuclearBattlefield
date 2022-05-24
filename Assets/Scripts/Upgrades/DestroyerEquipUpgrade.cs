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

    public override bool TryUpgrade(Creature creature, bool positive)
    {
        GameObject.Destroy(creature.GetComponentInChildren<MightyWeapon>());
        creature.gameObject.AddComponent<Destroyer>();
        UpgradesManager.PayPoints(cost);
        return true;
    }
}
