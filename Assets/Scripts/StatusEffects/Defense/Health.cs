using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : StatusEffect
{
    public double health;
    public double maxHealth;
    Animator animator;

    private void OnEnable()
    {
        animator = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        if (animator != null) animator.SetBool("Dead", health <= 0);
    }


    override public Action ProcessEvent(Action action)
    {
        if (action.id == ID.Attack)
        {
            if (action.prms.ContainsKey(Ind.PhysDmg))
            {
                health -= action.prms[Ind.PhysDmg];
            }
            if (action.prms.ContainsKey(Ind.PercentDmg))
            {
                health = action.prms[Ind.PercentDmg] * health;
            }
            if (action.prms.ContainsKey(Ind.TrueDmg))
            {
                health -= action.prms[Ind.TrueDmg];
            }
            if (action.prms.ContainsKey(Ind.Healing))
            {
                health = Math.Min(health + action.prms[Ind.Healing], maxHealth);
            }
        }
        
        if (action.id == ID.Query)
        {
            if (action.prms.ContainsKey(Ind.CanAct) && health <= 0)
            {
                action.prms[Ind.CanAct] = 0;
            }
            if (action.prms.ContainsKey(Ind.Dead) && health <= 0)
            {
                action.prms[Ind.Dead] = 1;
            }
        }
        if (action.id == ID.Description)
        {
            if (action.prms.ContainsKey(Ind.Tooltip))
            {
                action.Add(String.Format("Health: {0}", health));
            }
        }
        return action;
    }

    public void Heal()
    {
        health = maxHealth;
    }

    public override List<(Type, Type)> GetRequirements()
    {
        return null;
    }
}
