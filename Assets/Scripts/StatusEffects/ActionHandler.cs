using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHandler : MonoBehaviour
{
    public TopologicalWizard wizard; 
    
    public Action ProcessAction(Action action)
    {
        wizard = GameObject.Find("TopologicalWizard").GetComponent<TopologicalWizard>();
        var allComponents = new List<Module>(GetComponentsInChildren<Module>());
        allComponents.Sort((a, b) => (wizard.Compare(a.GetType(),b.GetType())));

        for (int i = 0; i < allComponents.Count; i++)
        {
            action = allComponents[i].ProcessEvent(action);
        }
        foreach (var item in action.effects.Values)
        {
            if (action.id == ID.Attack) item.Build(gameObject); 
        }
        return action;
    }
}
