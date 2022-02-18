using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrueAI : Controller
{
    public Battlemanager manager;

    public void Start()
    {
        manager = GetComponent<Battlemanager>();
    }

    public override void Activate(Creature creature)
    {
        Action action = GetAction(creature);
        Creature target = GetTarget(action);
        action.id = ID.Attack;
        manager.Play(target, action);
    }

    /// <summary>
    /// Decide which ability to use. At the moment its random chance.
    /// </summary>
    /// <param name="creature">Current creature planning a move</param>
    /// <returns>Chosen action</returns>
    public Action GetAction(Creature creature) {
        float rnd = UnityEngine.Random.value;
        var action = new Action(ID.AttackBuild);
        if (rnd > 0.5f) {
            action.Add(Ind.Basic, 0);            
        }
        else {
            action.Add(Ind.Special, 0);
        }
        return creature.GetComponent<ActionHandler>().ProcessAction(action); ;
    }
    
    /// <summary>
    /// Picks which creature to target. At the moment its random.
    /// </summary>
    /// <param name="action">Picked action</param>
    /// <returns>Targetted creature</returns>
    public Creature GetTarget(Action action) {

        List<Ind> keys = new List<Ind>(action.prms.Keys);
        List<(int,int,bool)> pos = TargetingSystem.Evaluate(keys,true);

        var enemyfound = false;

        Creature cre = null;
        while (!enemyfound)
        {
            int rnd = UnityEngine.Random.Range(0, pos.Count);
            int target_pos = TargetingSystem.GetCreatureByPosition(pos[(int)rnd].Item1, pos[(int)rnd].Item2);
            
            if (pos[(int)rnd].Item3)
            {
                cre = manager.enemyParty.GetParty()[target_pos].GetComponent<Creature>();
            }
            else
            {
                cre = manager.party.GetParty()[target_pos].GetComponent<Creature>();
            }
            if (!cre.Is(Ind.Dead))
            {
                enemyfound = true;
            } 
        }
        return cre;
    }        
}
