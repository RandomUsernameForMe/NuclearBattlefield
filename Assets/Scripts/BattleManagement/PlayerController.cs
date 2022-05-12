using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class managing everything in redards of manual targeting creatures by the player. 
/// It picks and highlights viable targets and makes them clickable.
/// </summary>
public class PlayerController : Controller
{
    public BattleManager manager;
    public Query actionBuffer;
    public Button cancelButton;
    public UIManager UIManager;

    /// <summary>
    /// The function is called after a target has been picked a also handles swaps.
    /// </summary>
    /// <param name="target">Targeted creature</param>
    public void PlayActionAfterTargetAquired(Creature target)
    {
        switch (actionBuffer.type)
        {
            case QueryType.Swap:
                SwitchPos(manager.GetCurrentCreature(), target);
                manager.CurrentCreaturePlays(target, new Query(QueryType.None));
                break;
            default:
                manager.CurrentCreaturePlays(target, actionBuffer);
                break;
        }
        manager.allyParty.ResetColors();
        manager.enemyParty.ResetColors();
    }

    /// <summary>
    /// Switch positions with target ally
    /// </summary>
    /// <param name="creature">Controlled creture</param>
    /// <param name="other">Ally to swap with</param>
    private void SwitchPos(Creature creature, Creature other) //TODO this may not belong here
    {
        Transform temp = creature.transform.parent;
        creature.transform.SetParent(other.transform.parent);
        other.transform.SetParent(temp);        
    }

    /// <summary>
    /// Generate a basic attack of a current creature playing
    /// </summary>
    public void GenerateBasicAttackAndPickTarget()
    {
        Query action = new Query(QueryType.AttackBuild);
        action.parameters.Add(QueryParameter.Basic,1);
        action = manager.GetCurrentCreature().GetComponent<QueryHandler>().ProcessQuery(action);
        PreparePossibleTargets(action);
    }

    /// <summary>
    /// Generate a special ability of a current creature playing
    /// </summary>
    public void GenerateSpecialAttackAndPickTarget()
    {
        Query action = new Query(QueryType.AttackBuild);
        action.parameters.Add(QueryParameter.Special, 1);
        action = manager.GetCurrentCreature().GetComponent<QueryHandler>().ProcessQuery(action);
        PreparePossibleTargets(action);
    }

    /// <summary>
    /// Prepare for picking an ally to swap with
    /// </summary>
    public void Swap()
    {
        List<QueryParameter> keys = new List<QueryParameter>();
        keys.Add(QueryParameter.Ally);
        List<int> pos = TargetingSystem.PickViableTargets(keys, manager.GetCurrentCreature().isEnemy);
        UIManager.GetComponent<UIManager>().LockButtons(true);
        actionBuffer = new Query(QueryType.Swap);
        PrepareCreaturesForClicks(pos);
    }



    /// <summary>
    /// Prepare for choosing a target for your picked ability or attack 
    /// </summary>
    /// <param name="action"></param>
    public void PreparePossibleTargets(Query action)
    {
        List<QueryParameter> keys = new List<QueryParameter>(action.parameters.Keys);
        List<int> pos = TargetingSystem.PickViableTargets(keys, manager.GetCurrentCreature().isEnemy);
        actionBuffer = action;
        UIManager.GetComponent<UIManager>().LockButtons(true);
        PrepareCreaturesForClicks(pos);
        cancelButton.gameObject.SetActive(true);
    }


    /// <summary>
    /// Targets need to be clickable so player can pick one
    /// </summary>
    /// <param name="pos">List of targets to highlight</param>
    private void PrepareCreaturesForClicks(List<int> pos)
    {
        var party = manager.allyParty.GetParty();
        var enemyParty = manager.enemyParty.GetParty();

        foreach (var item in pos)
        {            
            var target = TargetingSystem.GetCreatureByPosition(item, manager);
            if (!target.Is(QueryParameter.Dead)) target.Highlight();
        }        
    }

    public override void CreatureActs(Creature creature)
    {
        UIManager.PrepareAbilityControlPanel(creature);
    }
}
