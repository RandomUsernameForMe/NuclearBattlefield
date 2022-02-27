using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureStatus : MonoBehaviour
{
    public Text health;
    public GameObject canvas;
    private Creature creature;

    // Start is called before the first frame update
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
        Action action = new Action(ID.Query);
        action.Add(Ind.Dead, 0);

        if (action.prms[Ind.Dead] == 0 )
        {
            action = new Action(ID.Description);
            action.Add(Ind.Tooltip, 0);
            action = creature.ProcessAction(action);
            action.descs.Reverse();
            health.text = string.Join("\n", action.descs);
        }        
    }    
}


