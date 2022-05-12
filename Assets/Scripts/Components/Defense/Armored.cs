using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armored : ValueComponent
{
    override public Query ProcessQuery(Query query)
    {
        if (query.type == QueryType.Attack || query.type == QueryType.Question)
        {
            if (query.parameters.ContainsKey(QueryParameter.PhysDmg))
            {                
                query.parameters[QueryParameter.PhysDmg] -= value;
            } 
        }
        if (query.type == QueryType.Description)
        {            
            if (query.parameters.ContainsKey(QueryParameter.Tooltip))
            {
                query.Add(String.Format("Armor: {0}", value));
            }
        }
        return query;
    }

    public override List<(Type, Type)> GetRequirements()
    {
        var returnValue = new List<(Type, Type)>();
        returnValue.Add((typeof(Armored), typeof(Health)));
        return returnValue;
    }
}
