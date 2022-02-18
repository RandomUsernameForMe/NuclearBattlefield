using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalWeapon : Module
{
    public double atkDmg;

    public override List<(Type, Type)> GetRequirements()
    {
        var returnValue = new List<(Type, Type)>();
        returnValue.Add((typeof(PhysicalWeapon), typeof(Health)));
        return returnValue;
    }

    public override Action ProcessEvent(Action action)
    {
        if (action.id == ID.AttackBuild)
        {
            if (action.prms.ContainsKey(Ind.Basic))
            {
                action.Add(Ind.PhysDmg, atkDmg);
            }
        }
        if (action.id == ID.Description)
        {
            if (action.prms.ContainsKey(Ind.Basic))
            {
                action.Add("A physical attack.");
            }
            if (action.prms.ContainsKey(Ind.Tooltip))
            {
                action.Add(String.Format("Attack: {0} dmg", atkDmg));
            }
        }
        return action;
    }
}
