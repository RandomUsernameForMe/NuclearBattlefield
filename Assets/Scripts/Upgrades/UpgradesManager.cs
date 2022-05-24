using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manager responsible for generating buttons for upgrades for each character.
/// Upgrades and not hardcoded, but take into account the state and equipment of each character and generate appropriate possibilities.
/// </summary>
public class UpgradesManager : MonoBehaviour
{
    Party party;
    public float oX;
    public float oY;
    public GameObject btnPrefab;
    public int upgPoints;
    public TextMeshProUGUI points;

    private void OnEnable()
    {
        LevelManager.OnCampfireSceneLoaded += Load;
    }

    private void OnDisable()
    {
        LevelManager.OnCampfireSceneLoaded -= Load;
    }

    /// <summary>
    /// Main function generating and printing out all possible upgrades and corresponding UI buttons
    /// </summary>
    public void Load()
    {
        GetPoints();
        party = GameObject.Find("AllyParty").GetComponent<Party>();
        float offsetX = 1.5f * oX;
        
        foreach (var item in party.party)
        {
            GenerateOptions(item.GetComponentInChildren<Creature>(), offsetX);
            offsetX -= oX;
        }
    }

    /// <summary>
    /// Given a creature, generate all possible available upgrades and present them out as buttons
    /// </summary>
    /// <param name="item">Creatur to generate upgrades for</param>
    /// <param name="offsetX">Coordinates for where to generate buttons</param>
    private void GenerateOptions(Creature creature, float offsetX)
    {        
        var viableUpgradesList = new List<Upgrade>();
        var upgrades = UpgradeStorage.GetPositiveUpgrades();

        foreach (var item in upgrades)
        {
            if (item.IsConditionPassed(creature)) viableUpgradesList.Add(item.Upgrade);
        }        

        // After all options have been generates, create desired buttons
        float offsetY = 0;
        foreach (var upgrade in viableUpgradesList)
        {
            var carrier = GameObject.Find("ButtonCarrier");
            GameObject buttonObj = Instantiate(btnPrefab,carrier.transform,false);
            buttonObj.transform.position = new Vector3(buttonObj.transform.position.x+offsetX, buttonObj.transform.position.y+ offsetY, 0);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = upgrade.buttonText;
            var button = buttonObj.GetComponentInChildren<Button>();
            button.onClick.AddListener(delegate { UpgradeButtonPress(upgrade,creature,buttonObj); });
            buttonObj.GetComponentInChildren<UpgradeHolder>().upg = upgrade;
            offsetY += oY;
        }
    }

    public static bool Contains(Component[] list, Type type)
    {
        bool rv = false;
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].GetType() == type) rv = true;
        }
        return rv;
    }    

    public void UpdatePoints()
    {
        points.text = "Upgrade Points: " + upgPoints;
        var buttons = GetComponentsInChildren<UpgradeHolder>();

        // if you dont have enough points for some upgrades, disable those
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].upg.cost > upgPoints)
            {
                buttons[i].GetComponentInChildren<Button>().interactable = false;
            }
        }
    }

    public void UpgradeButtonPress(Upgrade upgrade, Creature creature, GameObject buttonObj)
    {
        var upgSuccessful = upgrade.TryUpgrade(creature,true);
        if (upgSuccessful)
        {
            PayPoints(upgrade.cost);
            UpdatePoints();
            buttonObj.SetActive(false);
        }
    }

    private void GetPoints()
    {
        var obj = GameObject.Find("LevelInfo");
        if (obj != null) upgPoints = obj.GetComponent<LevelInfo>().currUpgPoints;
    }

    public static void PayPoints(int cost)
    {
        GameObject.Find("Upgrader").GetComponentInChildren<UpgradesManager>().upgPoints -= cost;
        GameObject.Find("LevelInfo").GetComponent<LevelInfo>().currUpgPoints -= cost;
    }
}
