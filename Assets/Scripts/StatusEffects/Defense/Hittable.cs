using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : Module
{
    public Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    public override Action ProcessEvent(Action action)
    {
        if (action.id == ID.Attack)
        {
            if (action.prms.ContainsKey(Ind.PhysDmg) && action.prms[Ind.PhysDmg] > 0)
            {
                animator.SetTrigger("Hit");
            }
            if (action.effects.ContainsKey(Ind.Poison))
            {
                animator.SetTrigger("PoisonHit");
            }
            if (action.prms.ContainsKey(Ind.DestroyerUsed))
            {
                var anim = GameObject.Find("LevelInfo").GetComponent<DestroyerCount>();
                anim.CountUp();
            }
        }
        if (action.id == ID.Tick)
        {            
            if (action.prms.ContainsKey(Ind.TrueDmg) && action.prms[Ind.TrueDmg] > 0)
            {
                animator.SetTrigger("PoisonHit");
            }
        }

        return action;
    }
    public override List<(Type, Type)> GetRequirements()
    {
        var returnValue = new List<(Type, Type)>();
        returnValue.Add((typeof(Health), typeof(Hittable)));
        return returnValue;
    }
}
