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

public enum StatusParameter
{
    PhysDmg,
    MagicDmg,
    TrueDmg,
    PercentDmg,
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

public abstract class StatusEffect : MonoBehaviour
{
    abstract public List<(Type,Type)> GetRequirements();   

    abstract public Query ProcessQuery(Query query);
}
