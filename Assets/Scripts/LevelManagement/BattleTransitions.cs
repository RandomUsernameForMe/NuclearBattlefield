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
public class BattleTransitions: MonoBehaviour
{
    public BattleManager manager;
    public Dictionary<string, GameObject> bestiary;
    public GameObject clawEnemy;
    public GameObject smasherEnemy;
    public GameObject allyParty;
    public GameObject emptyParty;
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
        var obj = GameObject.Find("LevelInfo");
        if (obj != null && obj.GetComponent<PartyHolder>().party != null)
        {
            manager.enemyParty = obj.GetComponent<PartyHolder>().party;
            manager.enemyParty.transform.position = new Vector3(5, 0, 0);
            manager.enemyParty.SetBattlePose();
        }
        else
        {
            var temp = Instantiate(emptyParty);
            manager.enemyParty = temp.GetComponent<Party>();
            LoadEnemiesIntoParty(manager.enemyParty, lvl, bestiary);
            GameObject.Find("LevelInfo").GetComponent<PartyHolder>().party = manager.enemyParty;
            DontDestroyOnLoad(GameObject.Find("LevelInfo").GetComponent<PartyHolder>().party);
        }
        

        // If there is no party from previous scene, create a new one from a template
        var party = GameObject.Find("AllyParty");
        if (info.currLevel == 1 && party == null)
        {
            var g = Instantiate(this.allyParty);
            g.name = "AllyParty";

        }
        manager.loaded = true;
    }

    public static void LoadEnemiesIntoParty(Party enemies, int lvl, Dictionary<string, GameObject> bestiary)
    {
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
    }

    internal void HandleResults(BattleResults results)
    {
        if (results.result ==1) winCanvas.SetActive(true);
        else loseCanvas.SetActive(true);
    }

    public static bool DidPartyLose(Party party)
    {
        bool ded = true;
        foreach (var item in party.GetParty())
        {
            var query = new Query(QueryType.Question);
            query.Add(QueryParameter.Dead, 0);
            item.GetComponent<QueryHandler>().ProcessQuery(query);
            if (query.parameters[QueryParameter.Dead] == 0)
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
        manager.allyParty.FullReset();
        manager.enemyParty.FullReset();
        manager.CurrentCreaturSkips();
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
    }
}
