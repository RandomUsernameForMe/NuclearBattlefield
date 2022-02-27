using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoisonBlast: StatusEffect
{
    public double potency;
    public int duration;

    public override Action ProcessEvent(Action action)
    {
        if (action.id == ID.AttackBuild)
        {
            if (action.prms.ContainsKey(Ind.Special))
            {
                action.Add(Ind.Poison,new PoisonBuilder(potency, duration));
            }
        }
        if (action.id == ID.Description)
        {
            if (action.prms.ContainsKey(Ind.SpecialName))
            {
                action.Add("Poison Blast");
            }
            if (action.prms.ContainsKey(Ind.Special))
            {
                action.Add("Blasts an enemy with a powerful toxin.");
            }
            if (action.prms.ContainsKey(Ind.Tooltip))
            {
                action.Add(String.Format("Poison: {0} dmg, {1} turn(s)", potency, duration));
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

    public override Action ProcessEvent(Action action)
    {
        if (action.id == ID.Description)
        {
            if (action.prms.ContainsKey(Ind.Tooltip))
            {
                action.Add(String.Format("Poisioned: {0} potancy, {1} duration", potency, timer));
            }
        }
        return action;
    }

    public override Action Tick()
    {
        Action action = new Action(ID.Attack);
        action.Add(Ind.TrueDmg, potency);
        timer -= 1;
        return action;
    }

    public void Set(double potency, int duration)
    {
        this.potency = potency;
        this.timer = duration;
    }
}

class PoisonBuilder : Builder
{
    public int duration;
    public override void Build(GameObject obj)
    {
        obj.AddComponent<Poison>().Set(value, duration);
    }

    public PoisonBuilder(double pot, int dur)
    {
        value = pot;
        duration = dur;
    }
}
