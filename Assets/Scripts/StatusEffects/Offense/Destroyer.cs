using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : StatusEffect
{
    public override List<(Type, Type)> GetRequirements()
    {
        return null;
    }

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.AttackBuild)
        {
            if (action.parameters.ContainsKey(StatusParameter.Special))
            {
                action.Add(StatusParameter.Enemy, 1);
                action.Add(StatusParameter.PhysDmg, 200);
                action.Add(StatusParameter.DestroyerUsed, 1);
                GameObject.Find("Main Camera").GetComponent<Animation>().Play(); 
            }
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(StatusParameter.Special))
            {
                action.Add("Devastate an enemy.");
            }
            if (action.parameters.ContainsKey(StatusParameter.SpecialName))
            {
                action.Add("Destroyer");
            }
            if (action.parameters.ContainsKey(StatusParameter.Tooltip))
            {
                action.Add(String.Format("DESTROYER EQUIPPED"));
            }
        }
        return action;
    }
}
