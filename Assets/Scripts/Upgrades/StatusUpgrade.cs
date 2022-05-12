using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUpgrade<T> : GenericUpgrade where T:ValueComponent
{
    int upgradeValue;
    public StatusUpgrade(int val)
    {
        upgradeValue = val;
    }
    public override void Upgrade(Creature creature)
    {
        var cret = creature.GetComponentInChildren<T>();
        cret.Upgrade(upgradeValue);        
    }
}
