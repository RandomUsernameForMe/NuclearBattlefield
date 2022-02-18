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

    public override Action ProcessEvent(Action action)
    {
        if (action.id == ID.Query)
        {
            if (action.prms.ContainsKey(Ind.CanAct)) {
                action.prms[Ind.CanAct] = 0;
            }
        }
        if (action.id == ID.Description)
        {
            if (action.prms.ContainsKey(Ind.Tooltip))
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

    public override Action Tick()
    {
        timer -= 1;
        return new Action(ID.None);
    }
}

class StunBuilder : Builder
{
    public int duration;
    public override void Build(GameObject obj)
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