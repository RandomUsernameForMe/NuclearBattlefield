using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MightyWeapon : StatusEffect
{
    public double gigaStrength;

    public override Action ProcessEvent(Action action)
    {
        if (action.id == ID.AttackBuild)
        {
            if (action.prms.ContainsKey(Ind.Special))
            {
                action.Add(Ind.Close, 1);
                action.Add(Ind.PhysDmg, gigaStrength);
            }            
        }
        if (action.id == ID.Description)
        {
            if (action.prms.ContainsKey(Ind.Special))
            {
                action.Add("Smash enemy with massive strike.");
            }
            if (action.prms.ContainsKey(Ind.SpecialName))
            {
                action.Add("PowerStrike");
            }
            if (action.prms.ContainsKey(Ind.Tooltip))
            {
                action.Add(String.Format("PowerStrike: {0} dmg", gigaStrength));
            }
        }

        return action;
    }
    public override List<(Type, Type)> GetRequirements()
    {
        return null;
    }
}
