using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHandler : MonoBehaviour
{
    public TopologicalWizard wizard; 
    

    /// <summary>
    /// Pass an Action object into all StatusEffcets in a creature. The effects are visited in topological order by their topological requirements. 
    /// </summary>
    /// <param name="action">Action carrying parameters and Builders</param>
    /// <returns>Action usually modified by status effects</returns>
    public Action ProcessAction(Action action)
    {
        wizard = GameObject.Find("TopologicalWizard").GetComponent<TopologicalWizard>();
        var allComponents = new List<StatusEffect>(GetComponentsInChildren<StatusEffect>());

        //Custom sorting mechanism
        allComponents.Sort((a, b) => (wizard.Compare(a.GetType(),b.GetType())));

        for (int i = 0; i < allComponents.Count; i++)
        {
            // each status effect may alter the processed action and that is expected behavior
            action = allComponents[i].ProcessEvent(action); 
        }

        // Action may carry Builders, which will created a new effect on hosting Creature (eg. poisoned)
        foreach (var item in action.effects.Values)
        {
            if (action.id == ID.Attack) item.Build(gameObject); 
        }
        return action;
    }
}
