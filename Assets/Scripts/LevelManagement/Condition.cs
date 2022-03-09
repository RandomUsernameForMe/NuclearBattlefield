using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition
{
    public string name;
    public abstract bool Passed();
}


/// <summary>
/// Checks whether an object of specific name end cantains a specified StatusEffect
/// </summary>
public class HasStatus : Condition
{
    Type type;

    public HasStatus(Type type, string name)
    {
        this.name = name;
        this.type = type;
    }
    override public bool Passed()
    {
        var creatureObj = GameObject.Find(name);
        if (creatureObj != null)
        {
            var effects = creatureObj.GetComponentsInChildren<StatusEffect>();
            return UpgradesManager.Contains(effects, type);
        }
        else return false;
    }
}

/// <summary>
/// Checks whether an object of specific name contains a specific value of specific parameter. For example if health equals exactly 0.
/// </summary>
public class HasValue : Condition
{
    public StatusParameter parameter;
    public double value;

    public HasValue(StatusParameter parameter, double value, string name)
    {
        this.parameter = parameter;
        this.value = value;
        this.name = name;
    }

    public override bool Passed()
    {
        var creatureObj = GameObject.Find(name);
        if (creatureObj != null)
        {
            var creature = creatureObj.GetComponent<Creature>();
            var query = new Query(QueryType.Question);
            query.Add(parameter, 0);
            query = creature.ProcessQuery(query);
            return (query.parameters[parameter] == value);
        }
        else return false;
    }
}

/// <summary>
/// Checks the current level
/// </summary>
public class IsWhichLevel : Condition
{
    int level;
    public IsWhichLevel(int level)
    {
        this.level = level;
    }

    public override bool Passed()
    {
        var info = GameObject.Find("LevelInfo").GetComponent<LevelInfo>();
        return (info.currLevel == level && info.campfire == false);
    }
}
