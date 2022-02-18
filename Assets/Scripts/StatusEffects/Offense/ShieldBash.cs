using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBash : Module
{
    public float bashStrength;
    public int stunDuration;
    public float chance;
    public override List<(Type, Type)> GetRequirements()
    {
        var returnValue = new List<(Type, Type)>();
        returnValue.Add((typeof(ShieldBash), typeof(Health)));
        return returnValue;
    }

    public override Action ProcessEvent(Action action)
    {
        if (action.id == ID.AttackBuild)
        {
            if (action.prms.ContainsKey(Ind.Special))
            {
                action.Add(Ind.Close, 1);
                action.Add(Ind.Enemy, 1);
                action.Add(Ind.PhysDmg, bashStrength);
                action.Add(Ind.Stun,new StunBuilder(chance,stunDuration));
                //action.Add(Ind.Push,new Push());
            }
        }
        if (action.id == ID.Description)
        {
            if (action.prms.ContainsKey(Ind.Special))
            {
                action.Add("Bashes enemy with a shield. Has a chance to stun and pushes an enemy to the back.");
            }
            if (action.prms.ContainsKey(Ind.SpecialName))
            {
                action.Add("Shield Bash");
            }
            if (action.prms.ContainsKey(Ind.Tooltip))
            {
                action.Add(String.Format("Bash: {0} dmg, {1} turn stun", bashStrength, stunDuration));
            }
        }
        return action;
    }
}
