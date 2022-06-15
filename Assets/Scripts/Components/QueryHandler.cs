using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueryHandler : MonoBehaviour
{
    public TopologicalWizard wizard;
    List<Component> components;

    private void Start()
    {
        Load();
    }

    private void OnEnable()
    {
        BattleManager.OnBattleLoaded += Load;
    }

    private void OnDisable()
    {
        BattleManager.OnBattleLoaded -= Load;
    }

    private void Load()
    {
        components = new List<Component>(GetComponentsInChildren<Component>());
        wizard = GameObject.Find("TopologicalWizard").GetComponent<TopologicalWizard>();
    }

    /// <summary>
    /// Pass a Query object into all StatusEffcets in a creature. The effects are visited in topological order by their requirements. 
    /// </summary>
    /// <param name="query">Query carrying parameters and StatusBuilders</param>
    /// <returns>Query usually modified by status effects</returns>
    public Query ProcessQuery(Query query)
    {
        components = new List<Component>(GetComponentsInChildren<Component>());
        //Custom sorting mechanism
        components.Sort((a, b) => (wizard.Compare(a.GetType(),b.GetType())));


        for (int i = 0; i < components.Count; i++)
        {
            // each status effect may alter the processed query and that is expected behavior
            query = components[i].ProcessQuery(query); 
        }

        // Query may carry StatusBuilders, which will created a new effect on hosting Creature (eg. poisoned)
        foreach (var item in query.effects.Values)
        {
            if (query.type == QueryType.Attack)
            {
                item.BuildStatusEffect(gameObject);
                //components = new List<StatusEffect>(GetComponentsInChildren<StatusEffect>());
            } 
        }
        return query;
    }
}
