using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonAmplifier : Module
{
    public int power;

    public override List<(Type, Type)> GetRequirements()
    {
        return null;
    }

    public override Action ProcessEvent(Action action)
    {
        if (action.id == ID.AttackBuild)
        {
            if (action.prms.ContainsKey(Ind.Basic))
            {
                action.Add(Ind.PoisonAmp, power);
            }
        }
        if (action.id == ID.Description)
        {
            if (action.prms.ContainsKey(Ind.Basic))
            {
                action.Add("If the enemy is poisoned, amplifies the poison."); 
            }
            if (action.prms.ContainsKey(Ind.Tooltip))
            {
                action.Add(String.Format("Attacks amplify poison."));
            }
        }
        return action;
    }
}
