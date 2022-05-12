using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBash : Component
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
            if (action.parameters.ContainsKey(QueryParameter.Special))
            {
                action.Add(QueryParameter.Close, 1);
                action.Add(QueryParameter.Enemy, 1);
                action.Add(QueryParameter.PhysDmg, bashStrength);
                action.Add(QueryParameter.Stun,new StunBuilder(chance,stunDuration));
                //action.Add(Ind.Push,new Push());
            }
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(QueryParameter.Special))
            {
                action.Add("Bashes enemy with a shield. Has a chance to stun and pushes an enemy to the back.");
            }
            if (action.parameters.ContainsKey(QueryParameter.SpecialName))
            {
                action.Add("Shield Bash");
            }
            if (action.parameters.ContainsKey(QueryParameter.Tooltip))
            {
                action.Add(String.Format("Bash: {0} dmg, {1} turn stun", bashStrength, stunDuration));
            }
        }
        return action;
    }
}
