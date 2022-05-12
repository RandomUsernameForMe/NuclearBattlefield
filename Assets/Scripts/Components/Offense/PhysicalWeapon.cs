using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalWeapon : ValueComponent
{

    public override List<(Type, Type)> GetRequirements()
    {
        var returnValue = new List<(Type, Type)>();
        returnValue.Add((typeof(PhysicalWeapon), typeof(Health)));
        return returnValue;
    }

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.AttackBuild)
        {
            if (action.parameters.ContainsKey(QueryParameter.Basic))
            {
                action.Add(QueryParameter.PhysDmg, value);
            }
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(QueryParameter.Basic))
            {
                action.Add("A physical attack.");
            }
            if (action.parameters.ContainsKey(QueryParameter.Tooltip))
            {
                action.Add(String.Format("Attack: {0} dmg", value));
            }
        }
        return action;
    }
}
