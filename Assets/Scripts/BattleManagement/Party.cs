using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    public List<Transform> party;

    /// <summary>
    /// Called after every creature has passed its turn. This triggers timed effects such as poison or stun.
    /// </summary>
    public void Tick()
    {
        for (int i = 0; i < party.Count; i++)
        {
            var effects = party[i].GetComponentsInChildren<TimedEffect>();
            var partyCreatures = GetParty();
            GameObject creature = partyCreatures[i];

            for (int j = 0; j < effects.Length; j++)
            {
                var item = effects[j];
                Action action = item.Tick();
                creature.GetComponent<ActionHandler>().ProcessAction(action);
                CreatureStatus st = creature.GetComponentInChildren<CreatureStatus>();
                st.UpdateUI();
                if (item.timer == 0)
                {
                    Destroy(item);
                }
            }
        }
    }

    /// <summary>
    /// Called after this object has been loaded into battle scene.
    /// </summary>
    public void SetBattlePose()
    {
        var pos = transform.position;
        party[0].position = new Vector3(pos.x-3, pos.y+0,pos.z-3);
        party[1].position = new Vector3(pos.x-3, pos.y+0,pos.z+3);
        party[2].position = new Vector3(pos.x+3, pos.y+0,pos.z+3);
        party[3].position = new Vector3(pos.x+3, pos.y+0,pos.z-3);
    }

    /// <summary>
    /// Called after this object has been loaded into resting scene.
    /// </summary>
    public void SetFirePose()
    {
        party[0].position = new Vector3(4, -1, 2);
        party[1].position = new Vector3(1.5f, -1, 3);
        party[2].position = new Vector3(-1.5f, -1, 3);
        party[3].position = new Vector3(-4, -1, 2);
    }

    private void OnEnable()
    {
        LevelManager.OnBattleLoaded += SetBattlePose;
        LevelManager.OnBattleLoaded += ResetSpeed;
        LevelManager.OnBattleLoaded += ResetColors;
        LevelManager.OnCampfireLoaded += SetFirePose;
    }

    private void OnDisable()
    {
        LevelManager.OnBattleLoaded -= SetBattlePose;
        LevelManager.OnBattleLoaded -= ResetSpeed;
        LevelManager.OnBattleLoaded -= ResetColors;
        LevelManager.OnCampfireLoaded -= SetFirePose;
    }

    public void ResetSpeed() {
        var allParty = GetParty();
        for (int i = 0; i < party.Count; i++)
        {
            allParty[i].GetComponent<Creature>().ResetSpeed();
        }
    }

    public void ResetColors() {
        var allParty = GetParty();
        foreach (var item in allParty)
        {
            item.GetComponentInChildren<Highlighter>().Reset(); ;
        }
    }    

    public List<GameObject> GetParty()
    {
        var returnValue = new List<GameObject>();
        foreach (var item in party)
        {
            returnValue.Add(item.GetChild(0).gameObject);
        }
        return returnValue;
    }    

    internal void Reset()
    {
        ResetSpeed();
        ResetColors();
        var allParty = GetParty();
        foreach (var item in allParty)
        {
            item.GetComponentInChildren<Creature>().FullHeal(); ;
        }
    }
}
