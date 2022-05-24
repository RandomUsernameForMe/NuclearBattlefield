using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyModifier : MonoBehaviour
{

    List<UpgradeWithCondition> positiveUpgrades;
    List<UpgradeWithCondition> negativeUpgrades;

    public void Start()
    {
        positiveUpgrades = UpgradeStorage.GetPositiveUpgrades();
        negativeUpgrades = UpgradeStorage.GetNegativeUpgrades();
    }

    public Party ModifyPartyDifficulty(Party enemyParty, int upgradePoints)
    {
        if (upgradePoints ==0) return enemyParty;
        for (int i = 0; i < 5; i++)
        {
            int rnd = UnityEngine.Random.Range(0, 4);
            ChangeDifficultyForCreature(enemyParty.party[rnd].GetComponentInChildren<Creature>(), upgradePoints > 0,ref upgradePoints);
        }
        return enemyParty;
    }

    private void ChangeDifficultyForCreature(Creature creature, bool makeEnemiesHarder,ref int points)
    {
        List<UpgradeWithCondition> possibleUpgrades;
        if (makeEnemiesHarder)
        {
            possibleUpgrades = positiveUpgrades.FindAll(x => x.IsConditionPassed(creature));
        }
        else {
            possibleUpgrades = negativeUpgrades.FindAll(x => x.IsConditionPassed(creature));
        }
        if (possibleUpgrades.Count == 0) throw new Exception();
        for (int i = 0; i < possibleUpgrades.Count*2; i++)
        {
            int rnd = UnityEngine.Random.Range(0, possibleUpgrades.Count);
            var upgradeSuccesful = possibleUpgrades[rnd].TryApplyUpgrade(creature,makeEnemiesHarder);
            if (upgradeSuccesful)
            {
                points -= possibleUpgrades[rnd].Upgrade.cost;
            }
            continue;
        }
        
    }
}
