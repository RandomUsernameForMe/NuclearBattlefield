using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class managing creature description when hovered over. 
/// </summary>
public class CreatureStatsDescriptionPanel : MonoBehaviour
{
    public Text health;
    public GameObject canvas;
    private Creature creature;

    void Start()
    {        
        canvas.SetActive(false);
        UpdateUI();
    }

    /// <summary>
    /// Generates and updates the current status efects tooltip for the creature
    /// </summary>
    public void UpdateUI() {
        creature = GetComponentInParent<Creature>();
        Query action = new Query(QueryType.Question);
        action.Add(QueryParameter.Dead, 0);

        if (action.parameters[QueryParameter.Dead] == 0 )
        {
            action = new Query(QueryType.Description);
            action.Add(QueryParameter.Tooltip, 0);
            action = creature.ProcessQuery(action);
            action.descs.Reverse();
            health.text = string.Join("\n", action.descs);
        }        
    }    
}


