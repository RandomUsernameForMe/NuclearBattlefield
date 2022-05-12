using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoisonBlast: ValueComponent
{
    public int duration;

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.AttackBuild)
        {
            if (action.parameters.ContainsKey(QueryParameter.Special))
            {
                action.Add(QueryParameter.Poison,new PoisonBuilder(value, duration));
            }
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(QueryParameter.SpecialName))
            {
                action.Add("Poison Blast");
            }
            if (action.parameters.ContainsKey(QueryParameter.Special))
            {
                action.Add("Blasts an enemy with a powerful toxin.");
            }
            if (action.parameters.ContainsKey(QueryParameter.Tooltip))
            {
                action.Add(String.Format("Poison: {0} dmg, {1} turn(s)", value, duration));
            }
        }
        return action;
    }

    public override List<(Type,Type)> GetRequirements()
    {
        var returnValue = new List<(Type, Type)>();
        returnValue.Add((typeof(PoisonBlast), typeof(Health)));
        return returnValue;
    }
}

public class Poison : TimedEffect
{
    public double potency;

    public Poison(double potency, int poisonTimer)
    {
        this.potency = potency;
        this.timer = poisonTimer;
    }


    public override List<(Type, Type)> GetRequirements()
    {
        return null;
    }

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(QueryParameter.Tooltip))
            {
                action.Add(String.Format("Poisioned: {0} potancy, {1} duration", potency, timer));
            }
        }
        return action;
    }

    public override Query Tick()
    {
        Query action = new Query(QueryType.Attack);
        action.Add(QueryParameter.TrueDmg, potency);
        timer -= 1;
        return action;
    }

    public void Set(double potency, int duration)
    {
        this.potency = potency;
        this.timer = duration;
    }
}

class PoisonBuilder : ComponentBuilder
{
    public int duration;
    public override void BuildStatusEffect(GameObject obj)
    {
        obj.AddComponent<Poison>().Set(value, duration);
    }

    public PoisonBuilder(double pot, int dur)
    {
        value = pot;
        duration = dur;
    }
}
