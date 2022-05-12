using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public HealthBarsUI healthbars_ally;
    public HealthBarsUI healthbars_enemy;
    public BattleManager manager;

    public TextMeshProUGUI top_text;
    public TextMeshProUGUI ability_description_text;

    public Button attackButton;
    public Button specialButton;
    public Button skipButton;
    public Button swapButton;
    public Button cancelButton;

    float nextActionTime = 0;
    float period = 3000;

    public void Update()
    {        
        // Regular updates of UI
        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + period;
            RefreshUI();
        }
    }

    private void OnEnable()
    {
        BattleManager.OnCharacterFinishedTurn += RefreshUI;
        BattleManager.OnRoundEnded += RefreshUI;
        BattleManager.OnBattleLoaded += RefreshUI;
        BattleManager.OnCharacterFinishedTurn += HideCancelButton;
    }

    private void OnDisable()
    {
        BattleManager.OnCharacterFinishedTurn -= RefreshUI;
        BattleManager.OnRoundEnded -= RefreshUI;
        BattleManager.OnBattleLoaded -= RefreshUI;
        BattleManager.OnCharacterFinishedTurn -= HideCancelButton;
    }

    public void RefreshUI()
    {
        healthbars_ally.RefreshPanel(manager.allyParty);
        healthbars_enemy.RefreshPanel(manager.enemyParty);
    }

    public void PrepareAbilityControlPanel(Creature creature) {
        top_text.gameObject.SetActive(true);
        attackButton.interactable = true;
        specialButton.interactable = true;
        skipButton.interactable = true;
        swapButton.interactable = true;
        top_text.text = creature.creatureName + "'s turn";
        creature.GetComponent<Creature>().UpdateUI();
        var action = new Query(QueryType.Description);
        action.Add(QueryParameter.SpecialName, 0);
        action = manager.GetCurrentCreature().GetComponent<QueryHandler>().ProcessQuery(action);
        specialButton.GetComponentInChildren<TextMeshProUGUI>().text = string.Join(" ", action.descs);
    }

    public void ShowAbilityDescription() {
        var action = new Query(QueryType.Description);
        action.Add(QueryParameter.Basic, 0);
        action = manager.GetCurrentCreature().GetComponent<QueryHandler>().ProcessQuery(action);
        ability_description_text.text = string.Join(" ", action.descs);
        ability_description_text.gameObject.SetActive(true);
    }

    public void ShowSpecialAbilityDescription() {
        var action = new Query(QueryType.Description);
        action.Add(QueryParameter.Special, 0);
        action = manager.GetCurrentCreature().GetComponent<QueryHandler>().ProcessQuery(action);
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

    /// <summary>
    /// For canceling an attack, we need to reset targetting system and unlock buttons to pick a new ability.
    /// </summary>
    public void CancelAttack()
    {
        LockButtons(false);
        var party = manager.allyParty;
        var enemyParty = manager.enemyParty;
        party.ResetColors();
        enemyParty.ResetColors();
        HideCancelButton();
    }
    public void LockButtons(bool val)
    {
        attackButton.interactable = !val;
        specialButton.interactable = !val;
        skipButton.interactable = !val;
        swapButton.interactable = !val;
    }

    public void HideCancelButton()
    {
        cancelButton.gameObject.SetActive(false);
    }
}
