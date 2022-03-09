using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongWeapon : StatusEffect
{

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.AttackBuild)
        {
            action.Add(StatusParameter.Enemy, 1);
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(StatusParameter.Basic))
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
