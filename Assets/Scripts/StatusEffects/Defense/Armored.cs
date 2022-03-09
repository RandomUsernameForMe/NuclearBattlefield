using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armored : StatusEffect
{
    public int armor;
    override public Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.Attack)
        {
            if (action.parameters.ContainsKey(StatusParameter.PhysDmg))
            {                
                action.parameters[StatusParameter.PhysDmg] -= armor;
            } 
        }
        if (action.type == QueryType.Description)
        {            
            if (action.parameters.ContainsKey(StatusParameter.Tooltip))
            {
                action.Add(String.Format("Armor: {0}", armor));
            }
        }
        return action;
    }


    public override List<(Type, Type)> GetRequirements()
    {
        var returnValue = new List<(Type, Type)>();
        returnValue.Add((typeof(Armored), typeof(Health)));
        return returnValue;
    }
}
