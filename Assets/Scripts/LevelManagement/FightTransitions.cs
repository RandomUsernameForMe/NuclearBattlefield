using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manager responsible for transition for and from battle scene. 
/// Checks if win or lose conditions have been met. Triggers next level if player won.
/// Also load appropriate enemies into battle.
/// </summary>
public class FightTransitions: MonoBehaviour
{
    public Battlemanager manager;
    public Dictionary<string, GameObject> bestiary;
    public GameObject clawEnemy;
    public GameObject smasherEnemy;
    public GameObject party;
    public GameObject winCanvas;
    public GameObject loseCanvas;
    private LevelInfo info;


    private void Awake()
    {
        bestiary = new Dictionary<string, GameObject>();
        bestiary.Add("clawer", clawEnemy);
        bestiary.Add("bigger", smasherEnemy);
        info = GameObject.Find("LevelInfo").GetComponent<LevelInfo>();
        LoadLevel(info.currLevel);
    }

    /// <summary>
    /// Prepare what configuration of enemies the player will face. Depends on current level. 
    /// </summary>
    /// <param name="lvl">level number</param>
    private void LoadLevel(int lvl)
    {
        var enemies = manager.enemyParty;
        switch (lvl)
        {
            case 1:

                Instantiate(bestiary["clawer"], enemies.party[0].transform);
                Instantiate(bestiary["clawer"], enemies.party[1].transform);
                Instantiate(bestiary["clawer"], enemies.party[2].transform);
                Instantiate(bestiary["clawer"], enemies.party[3].transform);
                break;

            case 2:

                Instantiate(bestiary["clawer"], enemies.party[0].transform);
                Instantiate(bestiary["clawer"], enemies.party[1].transform);
                Instantiate(bestiary["bigger"], enemies.party[2].transform);
                Instantiate(bestiary["bigger"], enemies.party[3].transform);
                break;

            case 3:

                Instantiate(bestiary["bigger"], enemies.party[0].transform);
                Instantiate(bestiary["bigger"], enemies.party[1].transform);
                Instantiate(bestiary["bigger"], enemies.party[2].transform);
                Instantiate(bestiary["bigger"], enemies.party[3].transform);
                break;
        }

        // If there is no party from previous scene, create a new one from a template
        var party = GameObject.Find("AllyParty");
        if (info.currLevel == 1 && party == null)
        {
            var g = Instantiate(this.party);
            g.name = "AllyParty";

        }
        manager.loaded = true;
    }

    /// <summary>
    /// Very intensive way to chech if any party has lost
    /// </summary>
    private void Update()
    {
        if (DidPlayerWin())
        {
            winCanvas.SetActive(true);
        }

        if (DidEnemyWin())
        {
            loseCanvas.SetActive(true);
        }
    }

    /// <summary>
    /// Check if any ally creature is alive, which is required for continuing
    /// </summary>
    /// <returns>If the enemy has won</returns>
    private bool DidEnemyWin()
    {
        bool ded = true;
        foreach (var item in manager.party.GetParty())
        {
            var query = new Query(QueryType.Question);
            query.Add(StatusParameter.Dead,0);
            item.GetComponent<QueryHandler>().ProcessQuery(query);
            if (query.parameters[StatusParameter.Dead]==0)
            {
                ded = false;
            }
        }
        return ded;
    }

    /// <summary>
    /// Decides whether al enemy creautres are dead
    /// </summary>
    /// <returns>Returns if the player has won</returns>
    private bool DidPlayerWin()
    {
        bool ded = true;
        foreach (var item in manager.enemyParty.GetParty())
        {
            var query = new Query(QueryType.Question);
            query.Add(StatusParameter.Dead, 0);
            item.GetComponent<QueryHandler>().ProcessQuery(query);
            if (query.parameters[StatusParameter.Dead] == 0)
            {
                ded = false;
            }
        }
        return ded;
    }

    public void TransitionToCampfire()
    {
        info.currUpgPoints = info.currUpgPoints +info.upgPointsGain;
        info.campfire = true;
        info.gameObject.GetComponent<LevelManager>().LoadCampFire();
    }

    public void ResetLevel()
    {
        manager.party.Reset();
        manager.enemyParty.Reset();
        manager.CurrentCreaturSkips();
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
    }
}
