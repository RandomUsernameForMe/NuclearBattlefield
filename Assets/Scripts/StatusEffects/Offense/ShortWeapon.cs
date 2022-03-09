using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortWeapon : StatusEffect
{

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.AttackBuild)
        {
            if (action.parameters.ContainsKey(StatusParameter.Basic))
            {
                //action.Add(Ind.Close, 1);
                action.Add(StatusParameter.Enemy, 1);
            }            
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(StatusParameter.Basic))
            {
                action.Add("Hits a close enemy.");
            }
        }
        return action;
    }

    public override List<(Type, Type)> GetRequirements()
    {
        return null;
    }
}
