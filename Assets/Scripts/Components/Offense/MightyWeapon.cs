using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MightyWeapon : Component
{
    public double gigaStrength;

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.AttackBuild)
        {
            if (action.parameters.ContainsKey(QueryParameter.Special))
            {
                action.Add(QueryParameter.Close, 1);
                action.Add(QueryParameter.PhysDmg, gigaStrength);
            }            
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(QueryParameter.Special))
            {
                action.Add("Smash enemy with massive strike.");
            }
            if (action.parameters.ContainsKey(QueryParameter.SpecialName))
            {
                action.Add("PowerStrike");
            }
            if (action.parameters.ContainsKey(QueryParameter.Tooltip))
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
