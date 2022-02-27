using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPanel : MonoBehaviour
{
    public List<Slider> healthbars;

    /// <summary>
    /// Finds out 
    /// </summary>
    /// <param name="party"></param>
    public void RefreshPanel(Party party) {

        Slider healthbar;
        var partyO = party.GetParty();
        for (int j = 0; j < partyO.Count; j++)
        {            
            Creature creature = partyO[j].GetComponent<Creature>();
            healthbar = healthbars[j];
            healthbar.gameObject.GetComponentInChildren<Text>().text = creature.creature_name;
            healthbar.value = (float) creature.GetHealth() /(float)creature.GetMaxHealth();
        }
    }
}
