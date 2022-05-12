using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum QueryType
{
    Attack,
    Description,
    AttackBuild,
    Render,
    Animation,
    Question,
    Tick,
    Swap,
    None,
}

public enum QueryParameter
{
    PhysDmg,
    MagicDmg,
    TrueDmg,
    PercentDmg,
    CalcultedDmg,
    PoisonAmp,
    Poison, 
    Healing,
    Stun,
    Move,
    Claws,
    
    // Question 
    CanAct,
    Dead,
    DestroyerUsed,

    // Description
    SpecialName,
    Basic,
    Special,
    Description,
    Tooltip,

    // Targeting
    Close,
    Far,
    Enemy,
    Ally,
}

public abstract class Component : MonoBehaviour
{
    abstract public List<(Type,Type)> GetRequirements();   

    abstract public Query ProcessQuery(Query query);
}

public abstract class ValueComponent : Component
{
    public int value;
    public void Upgrade(int val)
    {
        value += val;
    }
}