using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ComponentCreator<T> : Upgrade where T : UpgradableComponent
{
    public override bool TryUpgrade(Creature creature, bool positive)
    {
        var comp = creature.GetComponentInChildren<T>();
        if (comp ==null )
        {
            var comps = creature.GetComponentsInChildren<UpgradableComponent>();
            if (comps.Length >= 5) return false;

            creature.gameObject.AddComponent<T>();
            return true;
        }
        else
        {
            return comp.TryUpgrade(positive);
        }       
        
    }

    public Type GetGenericType()
    {
        return typeof(T);
    }
}