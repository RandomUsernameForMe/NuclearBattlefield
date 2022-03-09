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


    override public Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.Attack)
        {
            if (action.parameters.ContainsKey(StatusParameter.PhysDmg))
            {
                health -= action.parameters[StatusParameter.PhysDmg];
            }
            if (action.parameters.ContainsKey(StatusParameter.PercentDmg))
            {
                health = action.parameters[StatusParameter.PercentDmg] * health;
            }
            if (action.parameters.ContainsKey(StatusParameter.TrueDmg))
            {
                health -= action.parameters[StatusParameter.TrueDmg];
            }
            if (action.parameters.ContainsKey(StatusParameter.Healing))
            {
                health = Math.Min(health + action.parameters[StatusParameter.Healing], maxHealth);
            }
        }
        
        if (action.type == QueryType.Question)
        {
            if (action.parameters.ContainsKey(StatusParameter.CanAct) && health <= 0)
            {
                action.parameters[StatusParameter.CanAct] = 0;
            }
            if (action.parameters.ContainsKey(StatusParameter.Dead) && health <= 0)
            {
                action.parameters[StatusParameter.Dead] = 1;
            }
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(StatusParameter.Tooltip))
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
