using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrader : MonoBehaviour
{
    Party party;
    public float oX;
    public float oY;
    public GameObject btnPrefab;
    public int upgPoints;
    public TextMeshProUGUI points;

    // Start is called before the first frame update
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
    ///  Very inefficent vay to always have current number of points 
    /// </summary>
    public void Update()
    {
        points.text = "Upgrade Points: " + upgPoints;
        var buttons = GetComponentsInChildren<UpgradeHolder>();
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].upg.cost > upgPoints)
            {
                buttons[i].GetComponentInChildren<Button>().interactable = false;
            }
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
        var options = new List<UpgradeBuilder>();
        
        // hard coded upgrade possibilities
        if (Contains(effects,typeof(ShieldBash))) 
        {
            options.Add(new BashUpgrader(5,1));
        }
        if (Contains(effects, typeof(PhysicalWeapon)))
        {
            options.Add(new AttackUpgrader(5));
        }
        if (Contains(effects, typeof(PoisonBlast)) && Contains(effects,typeof(PoisonAmplifier)))
        {
            options.Add(new PoisonUpgrader(1));
        }
        if (Contains(effects, typeof(MightyWeapon)))
        {
            options.Add(new PowerStrikeUpgrader(1));
        }

        // Here I need to find out whether creature is Dead
        var a = new Action(ID.Query);
        a.Add(Ind.Dead,0);
        a = item.GetComponentInChildren<Creature>().ProcessAction(a);
        if (Contains(effects, typeof(Health)) && a.prms[Ind.Dead]==0)
        {
            options.Add(new FullHeal());
        }
        if (Contains(effects, typeof(Health)) && a.prms[Ind.Dead] == 1)
        {
            options.Add(new Reviver());
        }
        if (Contains(effects, typeof(HealingWave)))
        {
            options.Add(new HealingUpgrader(10));
        }


        // After all options have been generates, create desired buttons
        float offsetY = 0;
        foreach (var i in options)
        {
            var carrier = GameObject.Find("ButtonCarrier");
            GameObject btn = Instantiate(btnPrefab,this.transform);
            btn.transform.position = new Vector3(960+offsetX, 450 + offsetY, 0);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = i.buttonText;
            btn.GetComponentInChildren<Button>().onClick.AddListener(delegate { i.Upgrade(item.GetComponentInChildren<Creature>()); });
            btn.GetComponentInChildren<Button>().onClick.AddListener(delegate { btn.SetActive(false); });
            btn.GetComponentInChildren<UpgradeHolder>().upg = i;
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

    public static void PayPoints(int cost)
    {
        GameObject.Find("Upgrader").GetComponentInChildren<Upgrader>().upgPoints -= cost;
        GameObject.Find("LevelInfo").GetComponent<LevelInfo>().currUpgPoints -= cost;
    }

    private void GetPoints()
    {
        upgPoints = GameObject.Find("LevelInfo").GetComponent<LevelInfo>().currUpgPoints;
    }
}



// Didnt need and want a separate file for upgrades
public abstract class UpgradeBuilder
{
    public string buttonText;
    public string descriptionText;
    public int cost;
    abstract public void Upgrade(Creature creature);

}

internal class BashUpgrader : UpgradeBuilder
{
    private int v;
    private int b;

    public BashUpgrader(int v, int b)
    {
        this.v = v;
        this.b = b;
        cost = 3;
        buttonText = "BASH HARDER";
        descriptionText = "Increase the power of your stun by " + v + " and the duration by " + b;
    }

    public override void Upgrade(Creature creature)
    {
        creature.GetComponentInChildren<ShieldBash>().bashStrength += v;
        creature.GetComponentInChildren<ShieldBash>().stunDuration += b;
        Upgrader.PayPoints(cost);
    }
}

internal class HealingUpgrader : UpgradeBuilder
{
    private int v;
    public HealingUpgrader(int v)
    {
        this.v = v;
        cost = 3;
        buttonText = "Better Healing";
        descriptionText = "Increase the power of your healing spell by " + v+"\n Costs "+cost+" to upgrade.";        
    }

    public override void Upgrade(Creature creature)
    {
        creature.GetComponentInChildren<HealingWave>().strength += v;        
    }
}

internal class PowerStrikeUpgrader : UpgradeBuilder
{
    private int v;
    public PowerStrikeUpgrader(int v)
    {
        this.v = v;
        cost = 4;
        buttonText = "Equip Destroyer!";
        descriptionText = "Just devastate. Costs " + cost + " to upgrade.";
    }

    public override void Upgrade(Creature creature)
    {
        GameObject.Destroy(creature.GetComponentInChildren<MightyWeapon>());
        creature.gameObject.AddComponent(typeof(Destroyer));
        Upgrader.PayPoints(cost);
    }
}

internal class PoisonUpgrader : UpgradeBuilder
{
    private int v;

    public PoisonUpgrader(int v)
    {
        this.v = v;
        cost = 3;
        buttonText = "Deadlier poison.";
        descriptionText = "Add both damage and duration of your poison. Costs " + cost + " to upgrade.";
    }

    public override void Upgrade(Creature creature)
    {
        creature.GetComponentInChildren<PoisonBlast>().potency += v;
        creature.GetComponentInChildren<PoisonBlast>().duration += v;
        creature.GetComponentInChildren<PoisonAmplifier>().power += v;
        Upgrader.PayPoints(cost);
    }
}

internal class AttackUpgrader : UpgradeBuilder
{
    private int v;

    public AttackUpgrader(int v)
    {
        this.v = v;
        cost = 2;
        buttonText = "Work out.";
        descriptionText = "Do "+v+ " more physical damage with your strike. Costs " + cost + " to upgrade.";
    }

    public override void Upgrade(Creature creature)
    {
        creature.GetComponentInChildren<PhysicalWeapon>().atkDmg += v;
        Upgrader.PayPoints(cost);
    }
}

internal class FullHeal : UpgradeBuilder
{
    private int v;

    public FullHeal()
    {        
        cost = 1;
        buttonText = "Heal";
        descriptionText = "Heals character. Costs " + cost + ".";
    }

    public override void Upgrade(Creature creature)
    {
        creature.GetComponentInChildren<Health>().Heal();
        Upgrader.PayPoints(cost);
    }
}

internal class Reviver : UpgradeBuilder
{
    private int v;

    public Reviver()
    {
        cost = 2;
        buttonText = "Revive";
        descriptionText = "Revives and fullheals character. Costs " + cost + ".";
    }

    public override void Upgrade(Creature creature)
    {
        creature.GetComponentInChildren<Health>().Heal();
        Upgrader.PayPoints(cost);
    }
}
