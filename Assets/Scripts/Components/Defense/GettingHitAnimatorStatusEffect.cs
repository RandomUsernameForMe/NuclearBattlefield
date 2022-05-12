using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingHitAnimatorStatusEffect : Component
{
    public Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    public override Query ProcessQuery(Query query)
    {
        if (query.type == QueryType.Attack)
        {
            if (query.parameters.ContainsKey(QueryParameter.PhysDmg) && query.parameters[QueryParameter.PhysDmg] > 0)
            {
                animator.SetTrigger("Hit");
            }
            if (query.effects.ContainsKey(QueryParameter.Poison))
            {
                animator.SetTrigger("PoisonHit");
            }
            if (query.parameters.ContainsKey(QueryParameter.DestroyerUsed))
            {
                var anim = GameObject.Find("LevelInfo").GetComponent<DestroyerCount>();
                anim.CountUp();
            }
        }
        if (query.type == QueryType.Tick)
        {            
            if (query.parameters.ContainsKey(QueryParameter.TrueDmg) && query.parameters[QueryParameter.TrueDmg] > 0)
            {
                animator.SetTrigger("PoisonHit");
            }
        }

        return query;
    }
    public override List<(Type, Type)> GetRequirements()
    {
        var returnValue = new List<(Type, Type)>();
        returnValue.Add((typeof(Health), typeof(GettingHitAnimatorStatusEffect)));
        return returnValue;
    }
}
