using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 
/// </summary>
public class AIController : Controller
{
    public Battlemanager manager;

    public void Start()
    {
        manager = GetComponent<Battlemanager>();
    }

    /// <summary>
    /// General function to handle everything when computer wants to decide what to play
    /// </summary>
    /// <param name="creature"></param>
    public override void CreatureActs(Creature creature)
    {
        Query query = PickActionToPlay(creature);
        Creature target = PickTarget(query);
        query.type = QueryType.Attack;
        manager.CurrentCreaturePlays(target, query);
    }


    public Query PickActionToPlay(Creature creature) 
    {
        float rnd = UnityEngine.Random.value;
        var action = new Query(QueryType.AttackBuild);

        // at the moment the choice is random
        if (rnd > 0.5f) {
            action.Add(StatusParameter.Basic, 0);            
        }
        else {
            action.Add(StatusParameter.Special, 0);
        }
        return creature.GetComponent<QueryHandler>().ProcessQuery(action); ;
    }
    
    public Creature PickTarget(Query action) 
    {
        List<StatusParameter> keys = new List<StatusParameter>(action.parameters.Keys);
        List<(int,int,bool)> pos = TargetingSystem.PickViableTargets(keys,true);
        var enemyfound = false;
        Creature cre = null;

        //at the moment its random
        while (!enemyfound)
        {
            int rnd = UnityEngine.Random.Range(0, pos.Count);
            int target_pos = PlayerController.GetCreatureByPosition(pos[(int)rnd].Item1, pos[(int)rnd].Item2);
            
            if (pos[(int)rnd].Item3)
            {
                cre = manager.enemyParty.GetParty()[target_pos].GetComponent<Creature>();
            }
            else
            {
                cre = manager.party.GetParty()[target_pos].GetComponent<Creature>();
            }
            if (!cre.Is(StatusParameter.Dead))
            {
                enemyfound = true;
            } 
        }
        return cre;
    }        
}
