using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : TimedEffect
{
    public Stun(int duration)
    {
        this.timer = duration;
    }
    public override List<(Type, Type)> GetRequirements()
    {
        var returnValue = new List<(Type, Type)>();
        returnValue.Add((typeof(Stun), typeof(Health)));
        return returnValue;
    }

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.Question)
        {
            if (action.parameters.ContainsKey(StatusParameter.CanAct)) {
                action.parameters[StatusParameter.CanAct] = 0;
            }
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(StatusParameter.Tooltip))
            {
                action.Add(String.Format("Stunned: {0} turn(s)", timer));
            }
        }
        return action;
    }

    internal void Set(int duration)
    {
        this.timer = duration;
    }

    public override Query Tick()
    {
        timer -= 1;
        return new Query(QueryType.None);
    }
}

class StunBuilder : StatusBuilder
{
    public int duration;
    public override void BuildStatusEffect(GameObject obj)
    {
        // TODO náhodu 
        obj.AddComponent<Stun>().Set(duration);
    }

    public StunBuilder(double chance, int dur)
    {
        value = chance;
        duration = dur;
    }
}