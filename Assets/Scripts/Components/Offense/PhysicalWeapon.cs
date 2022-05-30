using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalWeapon : UpgradableComponent
{
    public int power;
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
                action.Add(QueryParameter.PhysDmg, power);
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
                action.Add(String.Format("Attack: {0} dmg", power));
            }
        }
        return action;
    }

    public override bool TryUpgrade(bool positive)
    {

        if (positive) power += 5;
        else
        {
            if (power <= 10) return false;
            power -= 5;
        }
        return true;
    }
}
