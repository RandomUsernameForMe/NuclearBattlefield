using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentUpgrade<T> : Upgrade where T:UpgradableComponent
{
    public override bool TryUpgrade(Creature creature, bool positive)
    {
        var comp = creature.GetComponentInChildren<T>();
        var upgradeSuccesful = comp.TryUpgrade(positive);
        return upgradeSuccesful;
    }
}
