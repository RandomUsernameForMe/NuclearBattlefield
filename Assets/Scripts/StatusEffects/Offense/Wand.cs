using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : StatusEffect
{

    public override Action ProcessEvent(Action action)
    {
        if (action.id == ID.AttackBuild)
        {
            if (action.prms.ContainsKey(Ind.Special))
            {

            }
        }
        if (action.id == ID.Description)
        {
            if (action.prms.ContainsKey(Ind.Special))
            {
                action.Add("Targets any creature on the battlefield.");
            }
        }
        return action;
    }

    public override List<(Type, Type)> GetRequirements()
    {
        return null;
    }
}
