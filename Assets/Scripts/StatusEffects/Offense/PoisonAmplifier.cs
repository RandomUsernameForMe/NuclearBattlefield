using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonAmplifier : StatusEffect
{
    public int power;

    public override List<(Type, Type)> GetRequirements()
    {
        return null;
    }

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.AttackBuild)
        {
            if (action.parameters.ContainsKey(StatusParameter.Basic))
            {
                action.Add(StatusParameter.PoisonAmp, power);
            }
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(StatusParameter.Basic))
            {
                action.Add("If the enemy is poisoned, amplifies the poison."); 
            }
            if (action.parameters.ContainsKey(StatusParameter.Tooltip))
            {
                action.Add(String.Format("Attacks amplify poison."));
            }
        }
        return action;
    }
}
