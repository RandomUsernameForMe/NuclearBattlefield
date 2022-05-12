using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : ValueComponent
{
    public double health;
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
        double healthChange =0 ;
        if (action.type == QueryType.Attack || action.type == QueryType.Question)
        {
            if (action.parameters.ContainsKey(QueryParameter.PhysDmg))
            {
                healthChange = -action.parameters[QueryParameter.PhysDmg];
            }
            if (action.parameters.ContainsKey(QueryParameter.PercentDmg))
            {
                healthChange = -action.parameters[QueryParameter.PercentDmg] * health;
            }
            if (action.parameters.ContainsKey(QueryParameter.TrueDmg))
            {
                healthChange = -action.parameters[QueryParameter.TrueDmg];
            }
            if (action.parameters.ContainsKey(QueryParameter.Healing))
            {
                healthChange = Math.Min(action.parameters[QueryParameter.Healing], value-health);
            }
        }
        
        if (action.type == QueryType.Attack)
        {
            health += healthChange;
        }
        
        if (action.type == QueryType.Question)
        {
            
            action.Add(QueryParameter.CalcultedDmg, -healthChange);
        }

        if (action.type == QueryType.Question)
        {
            if (action.parameters.ContainsKey(QueryParameter.CanAct) && health >= 0)
            {
                action.parameters[QueryParameter.CanAct] = 1;
            }
            if (action.parameters.ContainsKey(QueryParameter.Dead) && health <= 0)
            {
                action.parameters[QueryParameter.Dead] = 1;
            }
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(QueryParameter.Tooltip))
            {
                action.Add(String.Format("Health: {0}", health));
            }
        }
        return action;
    }

    public void Heal()
    {
        health = value;
    }

    public override List<(Type, Type)> GetRequirements()
    {
        return null;
    }
}
