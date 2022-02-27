using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ID
{
    Attack,
    Description,
    AttackBuild,
    Render,
    Animation,
    Query,
    Tick,
    Swap,
    None,
}

public enum Ind
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
    
    // Query 
    CanAct,
    
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
    Dead,
    DestroyerUsed,
}

public abstract class StatusEffect : MonoBehaviour
{
    abstract public List<(Type,Type)> GetRequirements();   

    abstract public Action ProcessEvent(Action action);
}
