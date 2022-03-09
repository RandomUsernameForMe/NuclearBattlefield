using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MightyWeapon : StatusEffect
{
    public double gigaStrength;

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.AttackBuild)
        {
            if (action.parameters.ContainsKey(StatusParameter.Special))
            {
                action.Add(StatusParameter.Close, 1);
                action.Add(StatusParameter.PhysDmg, gigaStrength);
            }            
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(StatusParameter.Special))
            {
                action.Add("Smash enemy with massive strike.");
            }
            if (action.parameters.ContainsKey(StatusParameter.SpecialName))
            {
                action.Add("PowerStrike");
            }
            if (action.parameters.ContainsKey(StatusParameter.Tooltip))
            {
                action.Add(String.Format("PowerStrike: {0} dmg", gigaStrength));
            }
        }

        return action;
    }
    public override List<(Type, Type)> GetRequirements()
    {
        return null;
    }
}
