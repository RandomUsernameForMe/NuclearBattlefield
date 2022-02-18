using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Catalog
{
    public List<string> deck;
    private Catalog() { }
    public static Catalog instance = null;

    public static Catalog Instance()
    {
        if (instance == null)
        {
            instance = new Catalog();
        }
        return instance;
    }
}

public class UIManager : MonoBehaviour
{
    public HealthBarPanel healthbars_ally;
    public HealthBarPanel healthbars_enemy;
    public Battlemanager manager;

    public TextMeshProUGUI top_text;
    public TextMeshProUGUI ability_description_text;

    public Button attackButton;
    public Button specialButton;
    public Button skipButton;
    public Button swapButton;

    float nextActionTime = 0;
    float period = 3000;

    public void Update()
    {
        
        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + period;
            RefreshUI();
        }
    }

    public void RefreshUI()
    {
        healthbars_ally.RefreshPanel(manager.party);
        healthbars_enemy.RefreshPanel(manager.enemyParty);
    }

    public void PreparePanel(Creature creature) {
        top_text.gameObject.SetActive(true);
        attackButton.interactable = true;
        specialButton.interactable = true;
        skipButton.interactable = true;
        swapButton.interactable = true;
        top_text.text = creature.GetName() + "'s turn";
        creature.GetComponent<Creature>().UpdateUI();
        var action = new Action(ID.Description);
        action.Add(Ind.SpecialName, 0);
        action = manager.GetCurrentCreature().GetComponent<ActionHandler>().ProcessAction(action);
        specialButton.GetComponentInChildren<TextMeshProUGUI>().text = string.Join(" ", action.descs);
    }

    public void LockButtons()
    {
        attackButton.interactable = false;
        specialButton.interactable = false;
        skipButton.interactable = false;
        swapButton.interactable = false;
    }

    public void ShowAbilityDescription() {
        var action = new Action(ID.Description);
        action.Add(Ind.Basic, 0);
        action = manager.GetCurrentCreature().GetComponent<ActionHandler>().ProcessAction(action);
        ability_description_text.text = string.Join(" ", action.descs);
        ability_description_text.gameObject.SetActive(true);
    }

    public void ShowSpecialAbilityDescription() {
        var action = new Action(ID.Description);
        action.Add(Ind.Special, 0);
        action = manager.GetCurrentCreature().GetComponent<ActionHandler>().ProcessAction(action);
        ability_description_text.text = string.Join(" ", action.descs);
        ability_description_text.gameObject.SetActive(true);
    }

    public void HideAbilityDescription() {
        ability_description_text.gameObject.SetActive(false);
    }

    public void ShowSkipDescription() {
        ability_description_text.text = "Skip your round";
        ability_description_text.gameObject.SetActive(true);
    }

    public void ShowSwapDescription()
    {
        ability_description_text.text = "Swap position";
        ability_description_text.gameObject.SetActive(true);
    }
}
