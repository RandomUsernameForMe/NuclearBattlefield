using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armored : Module
{
    public int armor;
    override public Action ProcessEvent(Action action)
    {
        if (action.id == ID.Attack)
        {
            if (action.prms.ContainsKey(Ind.PhysDmg))
            {                
                action.prms[Ind.PhysDmg] -= armor;
            } 
        }
        if (action.id == ID.Description)
        {            
            if (action.prms.ContainsKey(Ind.Tooltip))
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
