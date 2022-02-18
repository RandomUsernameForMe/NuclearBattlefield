using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingWave : Module
{
    public double strength;

    public override Action ProcessEvent(Action action)
    {
        if (action.id == ID.AttackBuild)
        {
            if (action.prms.ContainsKey(Ind.Special))
            {
                action.Add(Ind.Healing, strength);
            }
        }
        if (action.id == ID.Description)
        {
            if (action.prms.ContainsKey(Ind.SpecialName))
            {
                action.Add("Healing Touch");
            }
            if (action.prms.ContainsKey(Ind.Special))
            {
                action.Add("Restores health.");
            }
            if (action.prms.ContainsKey(Ind.Tooltip))
            {
                action.Add(String.Format("Healing: {0} hp", strength));
            }
        }
        return action;
    }

    public override List<(Type, Type)> GetRequirements()
    {
        var returnValue = new List<(Type, Type)>();
        returnValue.Add((typeof(HealingWave), typeof(Health)));
        return returnValue;
    }
}
