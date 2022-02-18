using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetingSystem : Controller
{

    public Battlemanager manager;
    public Action actionHolder;

    public Battlemanager GetManager()
    {
        return manager;
    }

    /// <summary>
    /// The function is called after a target has been picked. Also handles swaps.
    /// </summary>
    /// <param name="other">Targeted creature</param>
    public void TargetAquired(Creature other)
    {
        switch (actionHolder.id)
        {
            case ID.Swap:
                SwitchPos(manager.GetCurrentCreature(), other);
                manager.Play(other, new Action(ID.None));
                break;
            default:
                manager.Play(other, actionHolder);
                break;
        }
        manager.party.ResetColors();
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
    public void Attack()
    {
        Action action = new Action(ID.AttackBuild);
        action.prms.Add(Ind.Basic,1);
        action = manager.GetCurrentCreature().GetComponent<ActionHandler>().ProcessAction(action);
        ChooseEnemy(action);
    }

    /// <summary>
    /// Generate a special ability of a current creature playing
    /// </summary>
    public void Special()
    {
        Action action = new Action(ID.AttackBuild);
        action.prms.Add(Ind.Special, 1);
        action = manager.GetCurrentCreature().GetComponent<ActionHandler>().ProcessAction(action);
        ChooseEnemy(action);
    }

    /// <summary>
    /// Prepare for picking an ally to swap with
    /// </summary>
    public void Swap()
    {
        List<Ind> keys = new List<Ind>();
        keys.Add(Ind.Ally);
        List<(int, int, bool)> pos = Evaluate(keys, manager.GetCurrentCreature().enemy);
        GetComponent<UIManager>().LockButtons();
        actionHolder = new Action(ID.Swap);
        PrepareForClicks(pos);
    }

    /// <summary>
    /// Prepare for choosing a target foir your picked ability or attack 
    /// </summary>
    /// <param name="action"></param>
    public void ChooseEnemy(Action action)
    {
        List<Ind> keys = new List<Ind>(action.prms.Keys);
        List<(int, int, bool)> pos = Evaluate(keys, manager.GetCurrentCreature().enemy);
        actionHolder = action;
        GetComponent<UIManager>().LockButtons();
        PrepareForClicks(pos);
    }

    /// <summary>
    /// Return available target positions for an attack or an ability. 
    /// </summary>
    /// <param name="pos">Grid coordinates plus a bool if ally or enemy side</param>
    /// <param name="is_enemy">If the one asking is enemy or frieds</param>
    /// <returns></returns>
    public static List<(int, int, bool)> Evaluate(List<Ind> pos, bool is_enemy)
    {
        List<(int, int, bool)> list = new List<(int, int, bool)> { (0, 0, false), (1, 0, false), (0, 1, false), (1, 1, false), (0, 0, true), (1, 0, true), (0, 1, true), (1, 1, true) };
        foreach (var item in pos)
        {
            switch (item)
            {
                case Ind.Close:
                    list.Remove((0, 0, false));
                    list.Remove((0, 1, false));
                    list.Remove((1, 0, true));
                    list.Remove((1, 1, true));
                    break;
                case Ind.Far:
                    list.Remove((1, 0, false));
                    list.Remove((1, 1, false));
                    list.Remove((0, 0, true));
                    list.Remove((0, 1, true));
                    break;
                case Ind.Ally:
                    list.Remove((0, 0, !is_enemy));
                    list.Remove((0, 1, !is_enemy));
                    list.Remove((1, 0, !is_enemy));
                    list.Remove((1, 1, !is_enemy));
                    break;
                case Ind.Enemy:
                    list.Remove((0, 0, is_enemy));
                    list.Remove((0, 1, is_enemy));
                    list.Remove((1, 0, is_enemy));
                    list.Remove((1, 1, is_enemy));
                    break;
            }
        }
        return list;
    }

    /// <summary>
    /// After finding targets, makes sure they are activated and highlighted
    /// </summary>
    /// <param name="pos">List of targets to highlight</param>
    private void PrepareForClicks(List<(int, int, bool)> pos)
    {
        var party = manager.party.GetParty();
        var enemyParty = manager.enemyParty.GetParty();

        foreach (var item in pos)
        {
            if (item.Item3)
            {
                enemyParty[GetCreatureByPosition(item.Item1, item.Item2)].GetComponentInChildren<Creature>().Activate();
            }
            else
            {
                party[GetCreatureByPosition(item.Item1, item.Item2)].GetComponentInChildren<Creature>().Activate();
            }
        }
    }

    public static int GetCreatureByPosition(int X, int Y)
    {
        return 2 * X + Y;
    }

    public override void Activate(Creature creature)
    {
        GetComponent<UIManager>().PreparePanel(creature);
    }
}
