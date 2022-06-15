using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStrike : UpgradableComponent
{
    float intensity = 0.5f;
    public override List<(Type, Type)> GetRequirements()
    {
        var returnValue = new List<(Type, Type)>();
        returnValue.Add((typeof(Health), typeof(FirstStrike)));
        return returnValue;
    }

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.AttackBuild)
        {
            var health = gameObject.GetComponentInParent<Creature>().GetHealth();
            var maxHealth = gameObject.GetComponentInParent<Creature>().GetMaxHealth();
            if (action.parameters.ContainsKey(QueryParameter.PhysDmg) && health == maxHealth)
            {
                action.parameters[QueryParameter.PhysDmg] = action.parameters[QueryParameter.PhysDmg] + action.parameters[QueryParameter.PhysDmg] * intensity;
            }
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(QueryParameter.Tooltip))
            {
                action.Add(String.Format("Deals {0}% more damage when full hp", intensity * 100));
            }
        }
        return action;
    }

    public override bool TryUpgrade(bool positive)
    {
        if (intensity <= 0.2)
        {
            Destroy(this);
            return true;
        }
        if (positive)
        {
            if (intensity >= 1) return false;
            intensity += 0.2f;
        }
        else intensity -= 0.2f;
        return true;
    }
}
