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

public class Battlemanager : MonoBehaviour
{
    public Party party;
    public Party enemyParty;
    
    public Creature currentCreature;
    private UIManager ui;

    private float lastMove;
    private bool busy = false;
    public bool loaded = false;

    private void Start()
    {
        Load();
    }

    void Update()
    {
        if (Time.time - lastMove > 1 && !busy)
        {
            busy = true;
            currentCreature.GetComponentInChildren<Highlighter>().SetToPlayingColor();
            currentCreature.DecideYourMove();
        }
    }

    /// <summary>
    /// Initiation at the start of a battle
    /// </summary>
    private void Load()
    {
        party = GameObject.Find("AllyParty").GetComponent<Party>();
        enemyParty = GameObject.Find("EnemyParty").GetComponent<Party>();
        SetNextCreature();
        ui = GetComponent<UIManager>();
        ui.RefreshUI();
        lastMove = Time.time;
    }

    
    /// <summary>
    /// Skips a turn of a character
    /// </summary>
    public void Skip(){
        ui.RefreshUI();
        lastMove = Time.time;
        busy = false;
        party.ResetColors();
        enemyParty.ResetColors();
        SetNextCreature();
    }

    /// <summary>
    /// Current creature performs a selected action towards target creature
    /// </summary>
    /// <param name="target">Picked target, ally or enemy</param>
    /// <param name="action">Picked or chosen action to perform</param>
    public void Play(Creature target, Action action)
    {
        ResetColors();

        if (action.id == ID.Swap)
        {
            Vector3 buffer = currentCreature.transform.position;
            currentCreature.Move(target.transform.position);
            target.Move(buffer);
        }

        if (action.id == ID.AttackBuild || action.id == ID.Attack)
        {
            // ID of action is currently "AttackBuilder", next think i want are attack animations 
            action.id = ID.Animation;
            currentCreature.GetComponent<ActionHandler>().ProcessAction(action);

            // And now i want the actually attack to proceed
            action.id = ID.Attack;
            target.GetComponent<ActionHandler>().ProcessAction(action);

        }

        target.UpdateUI();
        ui.RefreshUI();
        lastMove = Time.time;
        busy = false;
        SetNextCreature();        
    }

    /// <summary>
    /// Make a new creature take turn
    /// </summary>
    void SetNextCreature() {

        int max_speed = 0;
        Creature nextCreature = null;
        bool found = false;

        var playerCharacters = party.GetParty();
        var enemyCharacters = enemyParty.GetParty();


        // Search for a character with highest speed that havent moved
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
                NewTurn();              
            }
            else found = true;
        }
        
        nextCreature.GetComponent<Creature>().speed = 0;

        // Check if next creature in line can play (is stunned or dead?)
        Action action = new Action(ID.Query);
        action.Add(Ind.CanAct, 1);
        nextCreature.GetComponent<ActionHandler>().ProcessAction(action);

        if (action.prms[Ind.CanAct] == 0)
        {
            SetNextCreature(); // this is vulnerable to looping
        }
        else
        {
            currentCreature = nextCreature;            
        }       
    }    

    void NewTurn() {
        party.Tick();
        enemyParty.Tick();
        ResetSpeed();
        ui.RefreshUI();
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
