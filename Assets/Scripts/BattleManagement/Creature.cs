using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{    
    public string creature_name;
    public int speed;
    public int max_speed;

    public Controller targetingSystem;

    public bool enemy;
    public bool ai;


    private void Start()
    {
        FindTargetingSystem();
    }

    /// <summary>
    /// Need to find targetting system depending on controll mechanism.
    /// </summary>
    private void FindTargetingSystem()
    {
        if (ai)
        {
            targetingSystem = GameObject.Find("BattleManager").GetComponent<TrueAI>();
        }
        else
        {
            targetingSystem = GameObject.Find("BattleManager").GetComponent<TargetingSystem>();
        }        
    }

    /// <summary>
    /// In case creature changes its controls (eg. mind control spell), we need to refresh controller every time its about to act
    /// </summary>
    public void DecideYourMove()
    {
        FindTargetingSystem();
        targetingSystem.Activate(this);
    }

    /// <summary>
    /// Convenient way to ask if a creature is dead, sick, stuned or similar
    /// </summary>
    /// <param name="ind">The Status efect we are asking for</param>
    /// <returns>Answer</returns>
    public bool Is(Ind ind)
    {
        var c = new Action(ID.Query);
        c.Add(ind, 0);
        c = ProcessAction(c);
        if (c.prms[ind] == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public string GetName() 
    {
        return creature_name;
    }

    public void ResetSpeed() 
    {
        speed = max_speed;
    }

    public void UpdateUI()
    {
        GetComponentInChildren<CreatureStatus>().UpdateUI();
    }

    public void Activate() 
    {        
        GetComponentInChildren<Highlighter>().Activate();           
    }

    internal Action ProcessAction(Action action) 
    {
        return GetComponent<ActionHandler>().ProcessAction(action);
    }

    public double GetHealth() 
    {
        Health h = GetComponentInChildren<Health>();
        if (h != null)
        {
            return h.health;
        }
        else
        {
            return 0;
        }       
    }
    public double GetMaxHealth()
    {
        Health h = GetComponentInChildren<Health>();
        if (h != null) return h.maxHealth;
        return 0;
    }

    public void FullHeal()
    {
        Health h = GetComponentInChildren<Health>();
        h.health = h.maxHealth;
    }

    public void Move(Vector3 other)
    {
        transform.parent.position = other;
    }    
}
