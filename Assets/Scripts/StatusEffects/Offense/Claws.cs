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

    public override Action ProcessEvent(Action action)
    {
        if (action.id == ID.AttackBuild)
        {
            if (action.prms.ContainsKey(Ind.Special))
            {                
                action.Add(Ind.Enemy, 1);
                action.Add(Ind.Claws, new ClawedBuilder(strength));
            }
        }
        if (action.id == ID.Description)
        {
            if (action.prms.ContainsKey(Ind.Special))
            {
                action.Add("Cuts into damaged flesh. Does more damage to already clawed enemies.");
            }
            if (action.prms.ContainsKey(Ind.SpecialName))
            {
                action.Add("Claw attack");
            }
            if (action.prms.ContainsKey(Ind.Tooltip))
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

    public override Action ProcessEvent(Action action)
    {
        if (action.id == ID.Attack)
        {
            if (action.prms.ContainsKey(Ind.Claws))
            {
                action.prms[Ind.PhysDmg] += intensity;
                intensity += 1; 
            }
        }
        return action;
    }

    public override Action Tick()
    {
        timer = timer - 1;
        return new Action(ID.None);
    }

    internal void Set(int strength)
    {
        intensity = strength;
    }
}

public class ClawedBuilder : Builder
{
    int strength;
    public override void Build(GameObject obj)
    {
        obj.AddComponent<Clawed>().Set(strength);
    }

    public ClawedBuilder( int strength)
    {
        this.strength = strength;
    } 
}
