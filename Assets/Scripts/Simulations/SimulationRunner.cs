using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SimulationRunner : MonoBehaviour
{
    public BattleManager manager;
    public Controller AIControl;
    public Dictionary<string, GameObject> bestiary = new Dictionary<string, GameObject>();
    public GameObject weakEnemy;
    public GameObject strongEnemy;
    public Party enemyParty;
    public int simCount = 100;
    public float targetWinrate;
    public float winrateTolerance;
    public Button nextLevelButton;
    private PartyModifier modifier;


    private void Start()
    {
        modifier = GetComponent<PartyModifier>();
        bestiary.Add("clawer", weakEnemy);
        bestiary.Add("bigger", strongEnemy);
        if (GameObject.Find("LevelInfo").GetComponent<PartyHolder>() != null)
        {
            enemyParty = GameObject.Find("LevelInfo").GetComponent<PartyHolder>().party;
            enemyParty.transform.position += new Vector3(0, -5, 0);
        }
        StartSimulation();
    }
    
    public void ShowResults(List<BattleResults> results)
    {
        var gamesPlayed = results.Count;
        var text = GetComponentInChildren<Text>();

        int wins = results.Where(x => x.result.Equals(1)).Count();
        int loses = results.Where(x => x.result.Equals(2)).Count();
        float winrate = 100*((float)wins / gamesPlayed);
        text.text = String.Format("Played Games: {0} , Winrate: {1} %", gamesPlayed, winrate);
    }

    public void SetUpSimulation(int upgradepoints)
    {
        Party allyPartyCopy = Instantiate(GameObject.Find("AllyParty")).GetComponent<Party>();
        Party enemyParty = GenerateEnemyParty(upgradepoints);
        allyPartyCopy.FullReset();
        var transform = allyPartyCopy.gameObject.transform;
        transform.position += new Vector3(0, -5, 0);
        enemyParty.transform.position = allyPartyCopy.transform.position;
        SetupManager(manager, allyPartyCopy, enemyParty, AIControl);
        manager.LoadAndReset();
    }

    public void StartSimulation()
    {
        StartCoroutine(StartSimulationCoroutine());
    }

    public IEnumerator StartSimulationCoroutine()
    {
        SetUpSimulation(0);
        bool finished = false;
        while(!finished)
        {
            var results = new List<BattleResults>();
            for (int i = 0; i < simCount; i++)
            {
                while (manager.running == true)
                {
                    manager.RunOneTurn();
                }
                results.Add(manager.results);

                //ShowResults(results); Not needed
                manager.LoadAndReset();
                yield return null;
            }
            finished = AreSimulationsSatisfied(results);
            if (!finished)
            {
                var points = CalculateUpgradePoints(results);
                
                manager.enemyParty = modifier.ModifyPartyDifficulty(enemyParty, points);
                manager.LoadAndReset();                
            }
        }
        PrepareButtonForNextLevel(manager.enemyParty);
        Destroy(manager.allyParty.gameObject);
        
    }

    private void PrepareButtonForNextLevel(Party enemyParty)
    {
        nextLevelButton.interactable = true;
        nextLevelButton.GetComponent<UseGeneratedEnemiesButton>().nextEnemyGroup = enemyParty;
    }

    private bool AreSimulationsSatisfied(List<BattleResults> results)
    {
        int wins = results.Where(x => x.result.Equals(1)).Count();
        int loses = results.Where(x => x.result.Equals(2)).Count();
        float winrate = 100 * ((float)wins / (wins + loses));
        Debug.Log(String.Format("Winrate: {0}", winrate));
        return (Math.Abs(targetWinrate - winrate) < winrateTolerance);
    }

    public int CalculateUpgradePoints(List<BattleResults> results)
    {
        int wins = results.Where(x => x.result.Equals(1)).Count();
        int loses = results.Where(x => x.result.Equals(2)).Count();
        float winrate = 100 * ((float)wins / (wins + loses));
        return (int) (winrate-targetWinrate)/3; //změnit počet bodů zde
    }

    private void SetupManager(BattleManager manager,Party party, Party enemyParty, Controller aIControl)
    {
        manager.allyParty = party;
        manager.enemyParty = enemyParty;

        foreach (var item in party.party)
        {
            item.GetComponentInChildren<Creature>().isAI = true;
        }
    }

    private Party GenerateEnemyParty(int upgradepoints)
    {
        ClearEnemyParty();
        var obj = GameObject.Find("LevelInfo");
        if (obj != null && obj.GetComponent<PartyHolder>().party != null)
        {
            enemyParty = obj.GetComponent<PartyHolder>().party;
        }
        else
        {
            BattleTransitions.LoadEnemiesIntoParty(enemyParty, 1, bestiary);
        }
        BattleTransitions.LoadEnemiesIntoParty(enemyParty, 1, bestiary);
        modifier.ModifyPartyDifficulty(enemyParty,upgradepoints);
        return enemyParty;
    }    

    private void ClearEnemyParty()
    {
        foreach (var item in enemyParty.party)
        {
            if (item.childCount ==1) Destroy(item.GetChild(0).gameObject);
        }
    }
}
