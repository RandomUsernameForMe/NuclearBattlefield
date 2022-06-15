using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anger : UpgradableComponent
{
    float intensity = 0.2f;
    public override List<(Type, Type)> GetRequirements()
    {
        var returnValue = new List<(Type, Type)>();
        returnValue.Add((typeof(Health), typeof(Anger)));
        return returnValue;
    }

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.AttackBuild)
        {
            var health = gameObject.GetComponentInParent<Creature>().GetHealth();
            var maxHealth = gameObject.GetComponentInParent<Creature>().GetMaxHealth();
            if (action.parameters.ContainsKey(QueryParameter.PhysDmg) && 2*health <= maxHealth)
            {
                action.parameters[QueryParameter.PhysDmg] = action.parameters[QueryParameter.PhysDmg]+ action.parameters[QueryParameter.PhysDmg]*intensity;
            }
        }
        if (action.type == QueryType.Description)
        { 
            if (action.parameters.ContainsKey(QueryParameter.Tooltip))
            {
                action.Add(String.Format("Deals {0}% more dmg below half hp",intensity *100));
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
            intensity += 0.1f;
        }
        else intensity -= 0.1f;
        return true;
    }
}
