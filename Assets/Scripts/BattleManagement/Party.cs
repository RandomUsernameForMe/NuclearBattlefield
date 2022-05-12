using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Structure holding and managing a group of creatures, ally or foe.
/// Ally party is transfered in between senes, Enemy party is generated in each battle.
/// </summary>
public class Party : MonoBehaviour
{
    public List<Transform> party;

    private void OnEnable()
    {
        LevelManager.OnBattleSceneLoaded += SetBattlePose;
        LevelManager.OnBattleSceneLoaded += ResetSpeed;
        LevelManager.OnBattleSceneLoaded += ResetColors;
        LevelManager.OnCampfireSceneLoaded += SetCampfirePose;
        BattleManager.OnCharacterFinishedTurn += ResetColors;
        BattleManager.OnRoundEnded += ResetSpeed;

    }

    private void OnDisable()
    {
        LevelManager.OnBattleSceneLoaded -= SetBattlePose;
        LevelManager.OnBattleSceneLoaded -= ResetSpeed;
        LevelManager.OnBattleSceneLoaded -= ResetColors;
        LevelManager.OnCampfireSceneLoaded -= SetCampfirePose;
        BattleManager.OnCharacterFinishedTurn -= ResetColors;
        BattleManager.OnRoundEnded -= ResetSpeed;
    }

    /// <summary>
    /// In battle, characters stand in a square formation.
    /// </summary>
    public void SetBattlePose()
    {
        var pos = transform.position;
        party[0].position = new Vector3(pos.x - 3, pos.y + 0, pos.z - 3);
        party[1].position = new Vector3(pos.x - 3, pos.y + 0, pos.z + 3);
        party[2].position = new Vector3(pos.x + 3, pos.y + 0, pos.z + 3);
        party[3].position = new Vector3(pos.x + 3, pos.y + 0, pos.z - 3);
    }

    /// <summary>
    /// At campfire, characters are placed to be standing together next to the fire. 
    /// </summary>
    public void SetCampfirePose()
    {
        party[0].position = new Vector3(4, -1, 2);
        party[1].position = new Vector3(1.5f, -1, 3);
        party[2].position = new Vector3(-1.5f, -1, 3);
        party[3].position = new Vector3(-4, -1, 2);
    }

    /// <summary>
    /// Triggers timed effects such as poison or stun.
    /// </summary>
    public void TickTimedEffects()
    {
        for (int i = 0; i < party.Count; i++)
        {
            var effects = party[i].GetComponentsInChildren<TimedEffect>();
            var partyCreatures = GetParty();
            GameObject creature = partyCreatures[i];

            for (int j = 0; j < effects.Length; j++)
            {
                var item = effects[j];
                Query action = item.Tick();
                creature.GetComponent<QueryHandler>().ProcessQuery(action);
                CreatureStatsDescriptionPanel st = creature.GetComponentInChildren<CreatureStatsDescriptionPanel>();
                st.UpdateUI();
                if (item.timer == 0)
                {
                    Destroy(item);
                }
            }
        }
    }   

    public List<GameObject> GetParty()
    {
        var returnValue = new List<GameObject>();
        foreach (var item in party)
        {
            var gmo = item.GetChild(0).gameObject;
            returnValue.Add(gmo);
        }
        return returnValue;
    }    

    internal void FullReset()
    {
        ResetSpeed();
        ResetColors();
        var allParty = GetParty();
        foreach (var item in allParty)
        {
            item.GetComponentInChildren<Creature>().FullReset(); ;
        }
    }

    public void ResetSpeed()
    {
        var allParty = GetParty();
        for (int i = 0; i < party.Count; i++)
        {
            allParty[i].GetComponent<Creature>().ResetSpeed();
        }
    }

    public void ResetColors()
    {
        var allParty = GetParty();
        foreach (var item in allParty)
        {
            item.GetComponentInChildren<RingHighlighter>().Reset(); ;
        }
    }

    public override string ToString()
    {
        var HPs = new List<double>();
        foreach (var item in party)
        {
            HPs.Add(item.GetComponentInChildren<Creature>().GetMaxHealth());
        }

        var names = new List<string>();
        foreach (var item in party)
        {
            names.Add(item.GetComponentInChildren<Creature>().name);
        }

        return String.Format("({4}: {0}, {5}: {1}, {6}: {2}, {7}: {3})",
            HPs[0], HPs[1], HPs[2], HPs[3],
           names[0], names[1], names[2], names[3]);
    }
}
