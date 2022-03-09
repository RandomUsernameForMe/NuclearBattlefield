using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Position
{
    public Position(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X { get; }
    public double Y { get; }
    public override string ToString() => $"({X}, {Y})";
}

/// <summary>
/// Main manager of the battle and moves within. 
/// Controls the order of actions and assigns turns to creatures.
/// </summary>
public class Battlemanager : MonoBehaviour
{

    public Party party;
    public Party enemyParty;    
    public Creature currentCreature;
    private UIManager UI;
    private float lastMove;
    private bool busy = false;
    public bool loaded = false;

    private void Start()
    {
        Load();
    }   

    void Update()
    {
        // Minimum time is required between two creatures playing
        if (Time.time - lastMove > 1 && !busy)
        {
            busy = true;
            currentCreature.DecideYourMove();
        }
    }

    private void Load()
    {
        party = GameObject.Find("AllyParty").GetComponent<Party>();
        enemyParty = GameObject.Find("EnemyParty").GetComponent<Party>();
        NextCreaturesTurn();
        UI = GetComponent<UIManager>();
        UI.RefreshUI();
        lastMove = Time.time;
    }

    
    /// <summary>
    /// Skips a turn of a character
    /// </summary>
    public void CurrentCreaturSkips(){
        UI.RefreshUI();
        lastMove = Time.time;
        busy = false;
        party.ResetColors();
        enemyParty.ResetColors();
        NextCreaturesTurn();
    }

    /// <summary>
    /// Current creature performs a selected action towards target creature
    /// </summary>
    /// <param name="target">Picked target, ally or enemy</param>
    /// <param name="query">Picked or chosen action to perform</param>
    public void CurrentCreaturePlays(Creature target, Query query)
    {
        ResetColors();
        GetComponent<UIManager>().HideCancelButton();

        if (query.type == QueryType.Swap)
        {
            Vector3 buffer = currentCreature.transform.position;
            currentCreature.Move(target.transform.position);
            target.Move(buffer);
        }

        if (query.type == QueryType.Attack)
        {
            // I want to play possible animations first because processing query through creatures might modify it
            query.type = QueryType.Animation;
            currentCreature.GetComponent<QueryHandler>().ProcessQuery(query);

            // And now i want the actually attack to proceed
            query.type = QueryType.Attack;
            target.GetComponent<QueryHandler>().ProcessQuery(query);

        }

        target.UpdateUI();
        UI.RefreshUI();
        lastMove = Time.time;
        busy = false;
        NextCreaturesTurn();        
    }

    /// <summary>
    /// Make a new creature take turn
    /// </summary>
    void NextCreaturesTurn() {

        int max_speed = 0;
        Creature nextCreature = null;
        bool found = false;

        var playerCharacters = party.GetParty();
        var enemyCharacters = enemyParty.GetParty();

        // Search for a character with highest speed that havent moved this turn
        while (!found)
        {
            for (int i = 0; i < playerCharacters.Count; i++)
            {
                int speed = playerCharacters[i].GetComponent<Creature>().speed;
                if (speed > max_speed)
                {
                    max_speed = speed;
                    nextCreature = playerCharacters[i].GetComponent<Creature>();
                }
            }

            for (int i = 0; i < enemyCharacters.Count; i++)
            {
                int speed = enemyCharacters[i].GetComponent<Creature>().speed;
                if (speed > max_speed)
                {
                    max_speed = speed;
                    nextCreature = enemyCharacters[i].GetComponent<Creature>();
                }
            }
            
            if (nextCreature == null) {
                TriggerNewTurn();              
            }
            else found = true;
        }
        
        nextCreature.GetComponent<Creature>().speed = 0;
        Query action = new Query(QueryType.Question);
        action.Add(StatusParameter.CanAct, 1);
        nextCreature.GetComponent<QueryHandler>().ProcessQuery(action);

        // if chosen creature is unable to act, choose a next one
        if (action.parameters[StatusParameter.CanAct] == 0)
        {
            NextCreaturesTurn();
        }
        else
        {
            currentCreature = nextCreature;            
        }       
    }    

    /// <summary>
    /// After all creatures have played, their speeds are refreshed and all timed status effects (like poison) trigger
    /// </summary>
    void TriggerNewTurn() {
        party.Tick();
        enemyParty.Tick();
        ResetSpeed();
        UI.RefreshUI();
    }

    void ResetSpeed() {
        party.ResetSpeed();
        enemyParty.ResetSpeed();
    }

    void ResetColors() {
        party.ResetColors();
        enemyParty.ResetColors();
    }

    public Creature GetCurrentCreature() {
        return currentCreature;
    }
}
