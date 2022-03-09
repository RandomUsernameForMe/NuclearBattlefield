using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingWave : StatusEffect
{
    public double strength;

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.AttackBuild)
        {
            if (action.parameters.ContainsKey(StatusParameter.Special))
            {
                action.Add(StatusParameter.Healing, strength);
            }
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(StatusParameter.SpecialName))
            {
                action.Add("Healing Touch");
            }
            if (action.parameters.ContainsKey(StatusParameter.Special))
            {
                action.Add("Restores health.");
            }
            if (action.parameters.ContainsKey(StatusParameter.Tooltip))
            {
                action.Add(String.Format("Healing: {0} hp", strength));
            }
        }
        return action;
    }

    public override List<(Type, Type)> GetRequirements()
    {
        var returnValue = new List<(Type, Type)>();
        returnValue.Add((typeof(HealingWave), typeof(Health)));
        return returnValue;
    }
}
