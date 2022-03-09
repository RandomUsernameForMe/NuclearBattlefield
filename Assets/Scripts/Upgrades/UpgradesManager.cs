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

    public void Start()
    {        
        Load();
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
            GenerateOptions(item.gameObject, offsetX);
            offsetX -= oX;
        }
    }

    /// <summary>
    /// Given a creature, generate all possible available upgrades and present them out as buttons
    /// </summary>
    /// <param name="item">Creatur to generate upgrades for</param>
    /// <param name="offsetX">Coordinates for where to generate buttons</param>
    private void GenerateOptions(GameObject item, float offsetX)
    {        
        var effects = item.gameObject.GetComponentsInChildren<StatusEffect>();
        var upgradeBuilders = new List<UpgradeBuilder>();
        
        // hard coded upgrade possibilities
        if (Contains(effects,typeof(ShieldBash))) 
        {
            upgradeBuilders.Add(new BashUpgrade(5,1));
        }
        if (Contains(effects, typeof(PhysicalWeapon)))
        {
            upgradeBuilders.Add(new BasicAttackUpgrade(5));
        }
        if (Contains(effects, typeof(PoisonBlast)) && Contains(effects,typeof(PoisonAmplifier)))
        {
            upgradeBuilders.Add(new PoisonUpgrade(1));
        }
        if (Contains(effects, typeof(MightyWeapon)))
        {
            upgradeBuilders.Add(new PowerStrikeUpgrade(1));
        }
        if (Contains(effects, typeof(HealingWave)))
        {
            upgradeBuilders.Add(new HealingUpgrade(10));
        }

        // Here I need to find out whether creature is Dead
        var query = new Query(QueryType.Question);
        query.Add(StatusParameter.Dead,0);
        query = item.GetComponentInChildren<Creature>().ProcessQuery(query);

        if (Contains(effects, typeof(Health)) && query.parameters[StatusParameter.Dead] == 0)
        {
            upgradeBuilders.Add(new CampfireFullHeal());
        }
        if (Contains(effects, typeof(Health)) && query.parameters[StatusParameter.Dead] == 1)
        {
            upgradeBuilders.Add(new CampfireRevive());
        }

        // After all options have been generates, create desired buttons
        float offsetY = 0;
        foreach (var builder in upgradeBuilders)
        {
            var carrier = GameObject.Find("ButtonCarrier");
            GameObject buttonObj = Instantiate(btnPrefab,carrier.transform,false);
            buttonObj.transform.position = new Vector3(buttonObj.transform.position.x+offsetX, buttonObj.transform.position.y+ offsetY, 0);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = builder.buttonText;
            var button = buttonObj.GetComponentInChildren<Button>();
            button.onClick.AddListener(delegate { builder.Upgrade(item.GetComponentInChildren<Creature>()); });
            button.onClick.AddListener(delegate { UpdatePoints(); });
            button.onClick.AddListener(delegate { buttonObj.SetActive(false); });
            buttonObj.GetComponentInChildren<UpgradeHolder>().upg = builder;
            offsetY += oY;
        }
    }

    public static bool Contains(StatusEffect[] list, Type type)
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

    private void GetPoints()
    {
        upgPoints = GameObject.Find("LevelInfo").GetComponent<LevelInfo>().currUpgPoints;
    }

    public static void PayPoints(int cost)
    {
        GameObject.Find("Upgrader").GetComponentInChildren<UpgradesManager>().upgPoints -= cost;
        GameObject.Find("LevelInfo").GetComponent<LevelInfo>().currUpgPoints -= cost;
    }
}
