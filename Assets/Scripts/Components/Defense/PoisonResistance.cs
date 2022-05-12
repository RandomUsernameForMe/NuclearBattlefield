using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonResistance : Component
{
    /*
    
    */
    public double resistance;

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.Attack)
        {
            if (action.effects.ContainsKey(QueryParameter.Poison))
            {
                action.effects[QueryParameter.Poison].value = Math.Floor(action.effects[QueryParameter.Poison].value * (1-resistance));
            }
            if (action.parameters.ContainsKey(QueryParameter.PoisonAmp))
            {                
                Poison poison = GetComponent<Poison>();
                if (poison != null)
                {
                  poison.potency += action.parameters[QueryParameter.PoisonAmp];
                  poison.timer += (int)action.parameters[QueryParameter.PoisonAmp];
                }                
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
        returnValue.Add((typeof(PoisonResistance), typeof(Health)));
        return returnValue;
    }
}
