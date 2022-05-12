using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition
{
    public string name;
    public abstract bool isPassed();
}

public abstract class CreatureCondition : Condition
{
    public Creature creature;
} 


/// <summary>
/// Checks whether an object of specific name end cantains a specified StatusEffect
/// </summary>
public class HasStatus : CreatureCondition
{
    Type type;
    private Type t;

    public HasStatus(Type t)
    {
        this.t = t;
    }

    public HasStatus(Type type, Creature creature)
    {
        this.creature = creature;
        this.type = type;
    }
    public HasStatus(Type type, string name)
    {
        this.name = name;
        this.type = type;
    }
    override public bool isPassed()
    {
        if (creature == null)
        {
            var temp = GameObject.Find(name);
            if (temp == null) return false;
            creature = temp.GetComponent<Creature>();
        }
        return UpgradesManager.Contains(creature.GetComponentsInChildren<Component>(), type);
    }
}

/// <summary>
/// Checks whether an object of specific name contains a specific value of specific parameter. For example if health equals exactly 0.
/// </summary>
public class HasValue : CreatureCondition
{
    public QueryParameter parameter;
    public double value;

    public HasValue(QueryParameter parameter, double value, string name)
    {
        this.parameter = parameter;
        this.value = value;
        this.name = name;
    }

    public override bool isPassed()
    {
        if (creature == null)
        {
            var temp = GameObject.Find(name);
            if (temp == null) return false;
            creature = temp.GetComponent<Creature>();
        }
        var query = new Query(QueryType.Question);
        query.Add(parameter, 0);
        query = creature.ProcessQuery(query);
        return (query.parameters[parameter] == value);
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

    public override bool isPassed()
    {
        var info = GameObject.Find("LevelInfo").GetComponent<LevelInfo>();
        return (info.currLevel == level && info.isAtCampfire == false);
    }
}
