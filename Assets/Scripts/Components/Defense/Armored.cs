using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armored : UpgradableComponent
{
    public int armor= 3;
    override public Query ProcessQuery(Query query)
    {
        if (query.type == QueryType.Attack || query.type == QueryType.Question)
        {
            if (query.parameters.ContainsKey(QueryParameter.PhysDmg))
            {                
                query.parameters[QueryParameter.PhysDmg] -= armor;
            } 
        }
        if (query.type == QueryType.Description)
        {            
            if (query.parameters.ContainsKey(QueryParameter.Tooltip))
            {
                query.Add(String.Format("Armor: {0}", armor));
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

    public override bool TryUpgrade(bool positive)
    {
        if (armor <= 3)
        {
            Destroy(this);
            return true;
        }
        if (positive) armor += 3;
        else armor -= 3;
        return true;
    }
}
