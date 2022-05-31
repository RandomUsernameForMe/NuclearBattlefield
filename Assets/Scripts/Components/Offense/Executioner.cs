using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Executioner : Component
{
    float intensity = 0.5f;
    public override List<(Type, Type)> GetRequirements()
    {
        var returnValue = new List<(Type, Type)>();
        returnValue.Add((typeof(Executioner), typeof(Health)));
        return returnValue;
    }

    public override Query ProcessQuery(Query action)
    {
        if (action.type == QueryType.AttackBuild)
        {
            if (action.parameters.ContainsKey(QueryParameter.Special))
            {
                action.Add(QueryParameter.Executioner, intensity);
            }
        }
        if (action.type == QueryType.Description)
        {
            if (action.parameters.ContainsKey(QueryParameter.Special))
            {
                action.Add("Cuts into damaged flesh. Does more damage to already clawed enemies.");
            }
            if (action.parameters.ContainsKey(QueryParameter.SpecialName))
            {
                action.Add("Claw attack");
            }
            if (action.parameters.ContainsKey(QueryParameter.Tooltip))
            {
                action.Add(String.Format("Claws: {0} dmg, 2x to clawed enemies"));
            }
        }
        return action;
    }
}
