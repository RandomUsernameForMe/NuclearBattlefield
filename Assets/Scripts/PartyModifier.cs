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
        positiveUpgrades = new List<UpgradeWithCondition>();
        negativeUpgrades = new List<UpgradeWithCondition>();
        positiveUpgrades.Add(new UpgradeWithCondition(new HasStatus(typeof(Health), ""), new StatusUpgrade<Health>(5)));
        positiveUpgrades.Add(new UpgradeWithCondition(new HasStatus(typeof(Armored), ""), new StatusUpgrade<Armored>(3)));
        positiveUpgrades.Add(new UpgradeWithCondition(new HasStatus(typeof(PhysicalWeapon), ""), new StatusUpgrade<PhysicalWeapon>(5)));
        positiveUpgrades.Add(new UpgradeWithCondition(new HasStatus(typeof(PoisonBlast), ""), new StatusUpgrade<PoisonBlast>(5)));
        positiveUpgrades.Add(new UpgradeWithCondition(new HasStatus(typeof(HealingWave), ""), new StatusUpgrade<HealingWave>(10)));
        negativeUpgrades.Add(new UpgradeWithCondition(new HasStatus(typeof(Health), ""), new StatusUpgrade<Health>(-5)));
        negativeUpgrades.Add(new UpgradeWithCondition(new HasStatus(typeof(Armored), ""), new StatusUpgrade<Armored>(-3)));
        negativeUpgrades.Add(new UpgradeWithCondition(new HasStatus(typeof(PhysicalWeapon), ""), new StatusUpgrade<PhysicalWeapon>(-5)));
    }

    public Party ModifyPartyDifficulty(Party enemyParty, float difficultyLevel)
    {
        if (difficultyLevel == 1) return enemyParty;
        for (int i = 0; i < 5; i++)
        {
            int rnd = UnityEngine.Random.Range(0, 4);
            ChangeDifficultyForCreature(enemyParty.party[rnd].GetComponentInChildren<Creature>(), difficultyLevel > 1);
        }
        return enemyParty;
    }

    private void ChangeDifficultyForCreature(Creature creature, bool makeEnemiesHarder)
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
        int rnd = UnityEngine.Random.Range(0, possibleUpgrades.Count);
        possibleUpgrades[rnd].ApplyUpgrade(creature);
    }

    private object GenerateValidNerfs(Creature creature)
    {
        throw new NotImplementedException();
    }

    private object GenerateValidImprovements(Creature creature)
    {
        throw new NotImplementedException();
    }
}
