using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claws : Component
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
            if (action.parameters.ContainsKey(QueryParameter.Special))
            {                
                action.Add(QueryParameter.Enemy, 1);
                action.Add(QueryParameter.Claws, new ClawedBuilder(strength));
            }
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(QueryParameter.Special))
            {
                action.Add("Cuts into damaged flesh. Does more damage to already clawed enemies.");
            }
            if (action.parameters.ContainsKey(QueryParameter.SpecialName))
            {
                action.Add("Claw attack");
            }
            if (action.parameters.ContainsKey(QueryParameter.Tooltip))
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
            if (action.parameters.ContainsKey(QueryParameter.Claws))
            {
                action.parameters[QueryParameter.PhysDmg] += intensity;
                timer++;
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
        timer = 3;
    }
}

public class ClawedBuilder : ComponentBuilder
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
