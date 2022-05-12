using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimatorStatusEffect : Component
{
    public Animator animator;
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }
    public override Query ProcessQuery(Query query)
    {
        if (query.type == QueryType.Animation)
        {
            if (query.parameters.ContainsKey(QueryParameter.Basic))
            {
                animator.SetTrigger("Attack");
            }
            if (query.parameters.ContainsKey(QueryParameter.Special))
            {
                animator.SetTrigger("PowerStrike");
            }
        }
        return query;
    }

    public override List<(Type, Type)> GetRequirements()
    {
        return null;
    }
}
