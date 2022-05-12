using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : Component
{
    public override List<(Type, Type)> GetRequirements()
    {
        return null;
    }

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.AttackBuild)
        {
            if (action.parameters.ContainsKey(QueryParameter.Special))
            {
                action.Add(QueryParameter.Enemy, 1);
                action.Add(QueryParameter.PhysDmg, 200);
                action.Add(QueryParameter.DestroyerUsed, 1);
                GameObject.Find("Main Camera").GetComponent<Animation>().Play(); 
            }
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(QueryParameter.Special))
            {
                action.Add("Devastate an enemy.");
            }
            if (action.parameters.ContainsKey(QueryParameter.SpecialName))
            {
                action.Add("Destroyer");
            }
            if (action.parameters.ContainsKey(QueryParameter.Tooltip))
            {
                action.Add(String.Format("DESTROYER EQUIPPED"));
            }
        }
        return action;
    }
}
