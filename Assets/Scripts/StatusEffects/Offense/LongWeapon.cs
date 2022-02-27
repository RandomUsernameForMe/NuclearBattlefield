using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongWeapon : StatusEffect
{

    public override Action ProcessEvent(Action action)
    {
        if (action.id == ID.AttackBuild)
        {
            action.Add(Ind.Enemy, 1);
        }
        if (action.id == ID.Description)
        {
            if (action.prms.ContainsKey(Ind.Basic))
            {
                action.Add("Long Weapon strikes any enemy.");
            }
        }
        return action; 
    }
    public override List<(Type, Type)> GetRequirements()
    {
        var returnValue = new List<(Type, Type)>();
        returnValue.Add((typeof(Armored), typeof(Health)));
        return returnValue;
    }
}
