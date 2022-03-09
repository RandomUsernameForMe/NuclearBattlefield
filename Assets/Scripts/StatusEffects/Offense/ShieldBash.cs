using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBash : StatusEffect
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

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.AttackBuild)
        {
            if (action.parameters.ContainsKey(StatusParameter.Special))
            {
                action.Add(StatusParameter.Close, 1);
                action.Add(StatusParameter.Enemy, 1);
                action.Add(StatusParameter.PhysDmg, bashStrength);
                action.Add(StatusParameter.Stun,new StunBuilder(chance,stunDuration));
                //action.Add(Ind.Push,new Push());
            }
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(StatusParameter.Special))
            {
                action.Add("Bashes enemy with a shield. Has a chance to stun and pushes an enemy to the back.");
            }
            if (action.parameters.ContainsKey(StatusParameter.SpecialName))
            {
                action.Add("Shield Bash");
            }
            if (action.parameters.ContainsKey(StatusParameter.Tooltip))
            {
                action.Add(String.Format("Bash: {0} dmg, {1} turn stun", bashStrength, stunDuration));
            }
        }
        return action;
    }
}
