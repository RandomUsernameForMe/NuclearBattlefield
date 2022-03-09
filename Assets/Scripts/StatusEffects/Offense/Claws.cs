using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claws : StatusEffect
{
    public int strength;
    public override List<(Type, Type)> GetRequirements()
    {
        var returnValue = new List<(Type, Type)>();
        returnValue.Add((typeof(Claws), typeof(Health)));
        return returnValue;
    }

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.AttackBuild)
        {
            if (action.parameters.ContainsKey(StatusParameter.Special))
            {                
                action.Add(StatusParameter.Enemy, 1);
                action.Add(StatusParameter.Claws, new ClawedBuilder(strength));
            }
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(StatusParameter.Special))
            {
                action.Add("Cuts into damaged flesh. Does more damage to already clawed enemies.");
            }
            if (action.parameters.ContainsKey(StatusParameter.SpecialName))
            {
                action.Add("Claw attack");
            }
            if (action.parameters.ContainsKey(StatusParameter.Tooltip))
            {
                action.Add(String.Format("Claws: {0} dmg, 2x to clawed enemies", strength));
            }
        }
        return action;
    }
}

public class Clawed : TimedEffect
{
    int intensity;
    public override List<(Type, Type)> GetRequirements()
    {
        var returnValue = new List<(Type, Type)>();
        returnValue.Add((typeof(Claws), typeof(Armored)));
        return returnValue;
    }

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.Attack)
        {
            if (action.parameters.ContainsKey(StatusParameter.Claws))
            {
                action.parameters[StatusParameter.PhysDmg] += intensity;
                intensity += 1; 
            }
        }
        return action;
    }

    public override Query Tick()
    {
        timer = timer - 1;
        return new Query(QueryType.None);
    }

    internal void Set(int strength)
    {
        intensity = strength;
    }
}

public class ClawedBuilder : StatusBuilder
{
    int strength;
    public override void BuildStatusEffect(GameObject obj)
    {
        obj.AddComponent<Clawed>().Set(strength);
    }

    public ClawedBuilder( int strength)
    {
        this.strength = strength;
    } 
}
