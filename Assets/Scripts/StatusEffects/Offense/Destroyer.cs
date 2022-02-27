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

    public override Action ProcessEvent(Action action)
    {
        if (action.id == ID.AttackBuild)
        {
            if (action.prms.ContainsKey(Ind.Special))
            {
                action.Add(Ind.Enemy, 1);
                action.Add(Ind.PhysDmg, 200);
                action.Add(Ind.DestroyerUsed, 1);
                GameObject.Find("Main Camera").GetComponent<Animation>().Play(); 
            }
        }
        if (action.id == ID.Description)
        {
            if (action.prms.ContainsKey(Ind.Special))
            {
                action.Add("Devastate an enemy.");
            }
            if (action.prms.ContainsKey(Ind.SpecialName))
            {
                action.Add("Destroyer");
            }
            if (action.prms.ContainsKey(Ind.Tooltip))
            {
                action.Add(String.Format("DESTROYER EQUIPPED"));
            }
        }
        return action;
    }
}
