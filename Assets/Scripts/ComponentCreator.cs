using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ComponentCreator<T> : Upgrade where T : UpgradableComponent
{
    public override bool TryUpgrade(Creature creature, bool positive)
    {
        creature.gameObject.AddComponent<T>();
        return true;
    }
}