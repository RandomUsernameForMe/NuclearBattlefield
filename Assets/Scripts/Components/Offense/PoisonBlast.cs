using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoisonBlast: UpgradableComponent
{
    public int potency;
    public int duration;
    private int upgradeLevel = 1;

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.AttackBuild)
        {
            if (action.parameters.ContainsKey(QueryParameter.Special))
            {
                action.Add(QueryParameter.Poison,new PoisonBuilder(potency, duration));
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


        public override bool TryUpgrade(bool positive)
    {
        int newlvl = upgradeLevel;
        if (positive) newlvl++;
        else newlvl--;

        switch (newlvl)
        {
            case 0:
                Destroy(this);
                return true;
            case 1:
                duration = 2;
                potency = 10;
                break;
            case 2:
                duration = 2;
                potency = 20;
                break;
            case 3:
                duration = 100;
                potency = 20;
                break;
            case 4:
                return false;
        }
        upgradeLevel = newlvl;
        return true;
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
                action.Add(String.Format("Poisoned: {0} dmg, {1} turns", potency, timer));
            }
        }
        if (action.type == QueryType.Attack)
        {
            if (action.effects.ContainsKey(QueryParameter.Poison))
            {
                timer = Math.Max(2,timer);
                potency = Math.Max(potency, action.effects[QueryParameter.Poison].value);
                action.effects.Remove(QueryParameter.Poison);
            }
            if (action.parameters.ContainsKey(QueryParameter.PoisonAmp))
            {
                potency += 5;
                timer += 1;
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
        active = true;
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
