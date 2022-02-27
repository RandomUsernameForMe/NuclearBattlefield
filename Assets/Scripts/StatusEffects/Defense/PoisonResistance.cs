using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonResistance : StatusEffect
{
    /*
    
    */
    public double resistance;

    public override Action ProcessEvent(Action action)
    {
        if (action.id == ID.Attack)
        {
            if (action.effects.ContainsKey(Ind.Poison))
            {
                action.effects[Ind.Poison].value = Math.Floor(action.effects[Ind.Poison].value * (1-resistance));
            }
            if (action.prms.ContainsKey(Ind.PoisonAmp))
            {                
                Poison poison = GetComponent<Poison>();
                if (poison != null)
                {
                  poison.potency += action.prms[Ind.PoisonAmp];
                  poison.timer += (int)action.prms[Ind.PoisonAmp];
                }                
            }
        }
        if (action.id == ID.Description)
        {
            if (action.prms.ContainsKey(Ind.Tooltip))
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
