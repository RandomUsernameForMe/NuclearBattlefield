using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarsUI : MonoBehaviour
{
    public List<Slider> healthbars;

    public void RefreshPanel(Party party) {

        Slider healthbar;
        var partyO = party.GetParty();
        for (int j = 0; j < partyO.Count; j++)
        {            
            Creature creature = partyO[j].GetComponent<Creature>();
            healthbar = healthbars[j];
            healthbar.gameObject.GetComponentInChildren<Text>().text = creature.creatureName;
            healthbar.value = (float) creature.GetHealth() /(float)creature.GetMaxHealth();
        }
    }
}
