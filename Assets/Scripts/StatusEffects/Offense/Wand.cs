using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : StatusEffect
{

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.AttackBuild)
        {
            if (action.parameters.ContainsKey(StatusParameter.Special))
            {

            }
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(StatusParameter.Special))
            {
                action.Add("Targets any creature on the battlefield.");
            }
        }
        return action;
    }

    public override List<(Type, Type)> GetRequirements()
    {
        return null;
    }
}
