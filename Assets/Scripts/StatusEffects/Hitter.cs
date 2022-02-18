using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitter : Module
{
    public Animator animator;
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }
    public override Action ProcessEvent(Action action)
    {
        if (action.id == ID.Animation)
        {
            if (action.prms.ContainsKey(Ind.Basic))
            {
                animator.SetTrigger("Attack");
            }
            if (action.prms.ContainsKey(Ind.Special))
            {
                animator.SetTrigger("PowerStrike");
            }
        }
        return action;
    }

    public override List<(Type, Type)> GetRequirements()
    {
        return null;
    }
}
