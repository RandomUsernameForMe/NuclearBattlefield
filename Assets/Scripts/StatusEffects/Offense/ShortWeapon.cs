using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortWeapon : StatusEffect
{

    public override Action ProcessEvent(Action action)
    {
        if (action.id == ID.AttackBuild)
        {
            if (action.prms.ContainsKey(Ind.Basic))
            {
                //action.Add(Ind.Close, 1);
                action.Add(Ind.Enemy, 1);
            }            
        }
        if (action.id == ID.Description)
        {
            if (action.prms.ContainsKey(Ind.Basic))
            {
                action.Add("Hits a close enemy.");
            }
        }
        return action;
    }

    public override List<(Type, Type)> GetRequirements()
    {
        return null;
    }
}
