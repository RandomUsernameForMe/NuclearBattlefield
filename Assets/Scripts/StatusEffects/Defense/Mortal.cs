using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortal : StatusEffect
{

    public override Query ProcessQuery(Query action)
    {
        return action;
    }

    public override List<(Type, Type)> GetRequirements()
    {
        var returnValue = new List<(Type, Type)>();
        returnValue.Add((typeof(Health), typeof(Mortal)));
        return returnValue;
    }
}
