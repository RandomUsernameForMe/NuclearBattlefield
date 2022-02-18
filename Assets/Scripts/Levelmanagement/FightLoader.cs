using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightLoader: MonoBehaviour
{
    public Battlemanager m;

    public Dictionary<string, GameObject> bestiary;
    public GameObject clawEnemy;
    public GameObject strongerEnemy;

    public GameObject party;

    public GameObject winCanvas;
    public GameObject loseCanvas;
    private LevelInfo info;


    private void Awake()
    {
        bestiary = new Dictionary<string, GameObject>();
        bestiary.Add("clawer", clawEnemy);
        bestiary.Add("bigger", strongerEnemy);
        info = GameObject.Find("LevelInfo").GetComponent<LevelInfo>();
        LoadLevel(info.currLevel);
    }

    /// <summary>
    /// Prepare what configuration of enemies the player will face. 
    /// </summary>
    /// <param name="lvl">level number</param>
    private void LoadLevel(int lvl)
    {
        var enemies = m.enemyParty;
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

        // If thereis no party from previous scene, create a new one from a template
        var p = GameObject.Find("AllyParty");
        if (info.currLevel == 1 && p == null)
        {
            var g = Instantiate(party);
            g.name = "AllyParty";

        }
        m.loaded = true;
    }

    // TODO, this is very inefficent
    private void Update()
    {
        if (PlayerWon())
        {
            winCanvas.SetActive(true);
        }

        if (EnemyWon())
        {
            loseCanvas.SetActive(true);
        }
    }

    public void ResetLevel()
    {
        m.party.Reset();
        m.enemyParty.Reset();
        m.Skip();
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
    }

    /// <summary>
    /// Decide whether all player creatures are dead
    /// </summary>
    /// <returns>Returns if the enemy has won</returns>
    private bool EnemyWon()
    {
        bool ded = true;
        foreach (var item in m.party.party)
        {
            var action = new Action(ID.Query);
            action.Add(Ind.Dead,0);
            item.GetComponentInChildren<ActionHandler>().ProcessAction(action);
            if (action.prms[Ind.Dead]==0)
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
    private bool PlayerWon()
    {
        bool ded = true;
        foreach (var item in m.enemyParty.party)
        {
            var action = new Action(ID.Query);
            action.Add(Ind.Dead, 0);
            item.GetComponentInChildren<ActionHandler>().ProcessAction(action);
            if (action.prms[Ind.Dead] == 0)
            {
                ded = false;
            }
        }
        return ded;
    }

    /// <summary>
    /// Loads desired enemies into their appropriate spots
    /// </summary>
    /// <param name="input">List of desired enemies to load</param>
    public void LoadEnemies(List<string> input)
    {
        var enemies = m.enemyParty.party;
        for (int i = 0; i < input.Count; i++)
        {
            Instantiate(bestiary[input[i]], enemies[i]);
        }
    }

    /// <summary>
    /// TODO
    /// </summary>
    public void NextLevel()
    {
        info.currUpgPoints = info.currUpgPoints +info.upgPointsGain;
        info.campfire = true;
        info.gameObject.GetComponent<LevelManager>().LoadCampFire();
    }
}
