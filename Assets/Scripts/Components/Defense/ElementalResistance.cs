using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalResistance : UpgradableComponent
{
    public double resistance;

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.Attack)
        {
            if (action.effects.ContainsKey(QueryParameter.Poison))
            {
                action.effects[QueryParameter.Poison].value = Math.Floor(action.effects[QueryParameter.Poison].value * (1-resistance));
            }
            if (action.effects.ContainsKey(QueryParameter.FireDmg))
            {
                action.effects[QueryParameter.FireDmg].value = Math.Floor(action.effects[QueryParameter.FireDmg].value * (1 - resistance));
            }
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(QueryParameter.Tooltip))
            {
                action.Add(String.Format("Poison resist: {0} %", resistance*100));
            }
        }
        return action;
    }

    public override List<(Type, Type)> GetRequirements()
    {
        var returnValue = new List<(Type, Type)>();
        returnValue.Add((typeof(ElementalResistance), typeof(Health)));
        return returnValue;
    }

    public override bool TryUpgrade(bool positive)
    {
        if (resistance <= 0.1)
        {
            Destroy(this);
            return true;
        }
        if (positive) resistance += 0.1;
        else resistance -= 0.1;
        return true;
    }
}
