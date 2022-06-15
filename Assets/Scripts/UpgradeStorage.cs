using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStorage
{
    static List<UpgradeWithCondition> positive;
    static List<UpgradeWithCondition> negative;

    public static List<UpgradeWithCondition> GetPositiveUpgrades()
    {

        if (positive == null)
        {
            var positiveUpgrades = new List<UpgradeWithCondition>();
            positiveUpgrades.Add(new UpgradeWithCondition(
                new ComponentCondition<Health>(), 
                new ComponentUpgrade<Health>(),
                2,
                "Raise Health", 
                "Raises maximum health by 5, costs 2 points."));
            positiveUpgrades.Add(new UpgradeWithCondition(
                new ComponentCondition<Health>(), 
                new ComponentCreator<Armored>(),
                2,
                "Armor Up", 
                "You reduce physical damage by 3 more, costs 1 points."));
            positiveUpgrades.Add(new UpgradeWithCondition(
                new ComponentCondition<PhysicalWeapon>(), 
                new ComponentUpgrade<PhysicalWeapon>(),
                2,
                "Sharpen Weapon", 
                "Your weapon will deal 5 more physical damage, costs 2 points."));
            positiveUpgrades.Add(new UpgradeWithCondition(
                new ComponentCondition<PoisonBlast>(), 
                new ComponentUpgrade<PoisonBlast>(),
                2, 
                "Brew better poison", 
                "Increases strength or suration of your poison, costs 2 points."));
            positiveUpgrades.Add(new UpgradeWithCondition(
                new ComponentCondition<HealingWave>(), 
                new ComponentUpgrade<HealingWave>(),
                2,
                "Improve Healing",
                "You heal for 10 more health, costs 2 points."));
            positiveUpgrades.Add(new UpgradeWithCondition(
                new ComponentCondition<PhysicalWeapon>(),
                new ComponentCreator<FirstStrike>(),
                1,
                "Get First Strike",
                "Deal more damage while max HP, costs 1 point"));
           
            positiveUpgrades.Add(new UpgradeWithCondition(
                new ComponentCondition<PhysicalWeapon>(),
                new ComponentCreator<Anger>(),
                1,
                "Get angry",
                "Deal more damage while below half hp, costs 1 point"));
            
            positiveUpgrades.Add(new UpgradeWithCondition(
                new ComponentCondition<PowerStrike>(),
                new ComponentUpgrade<PowerStrike>(),
                2,
                "Upgrade PowerStrike",
                "Smash even more, costs 2 points."));
            positiveUpgrades.Add(new UpgradeWithCondition(
                new ComponentCondition<Claws>(),
                new ComponentUpgrade<Claws>(),
                2,
                "",
                ""));
            positiveUpgrades.Add(new UpgradeWithCondition(
                new ComponentCondition<PhysicalWeapon>(),
                new ComponentCreator<FieryWeapons>(),
                2,
                "Enflame Weapons",
                "Set your weapons ablaze. Deal bonus fire damage that ignores armor. Costs 2 points"));
            positiveUpgrades.Add(new UpgradeWithCondition(
                new ComponentCondition<Health>(),
                new ComponentCreator<ElementalResistance>(),
                2,
                "",
                ""));

            positive = positiveUpgrades;
        }
        return positive;
    }

    public static List<UpgradeWithCondition> GetNegativeUpgrades()
    {

        if (negative == null)
        {
            var negativeUpgrades = new List<UpgradeWithCondition>();
            negativeUpgrades.Add(new UpgradeWithCondition(new ComponentCondition<Health>(), new ComponentUpgrade<Health>(),2));
            negativeUpgrades.Add(new UpgradeWithCondition(new ComponentCondition<Armored>(), new ComponentUpgrade<Armored>(),2));
            negativeUpgrades.Add(new UpgradeWithCondition(new ComponentCondition<PhysicalWeapon>(), new ComponentUpgrade<PhysicalWeapon>(),2));
            negativeUpgrades.Add(new UpgradeWithCondition(new ComponentCondition<Anger>(), new ComponentUpgrade<Anger>(), 1));
            negativeUpgrades.Add(new UpgradeWithCondition(new ComponentCondition<FirstStrike>(), new ComponentUpgrade<FirstStrike>(), 1));
            negativeUpgrades.Add(new UpgradeWithCondition(new ComponentCondition<PowerStrike>(), new ComponentUpgrade<PowerStrike>(), 2));
            negativeUpgrades.Add(new UpgradeWithCondition(new ComponentCondition<Claws>(), new ComponentUpgrade<Claws>(), 2));
            negativeUpgrades.Add(new UpgradeWithCondition(new ComponentCondition<FieryWeapons>(), new ComponentUpgrade<FieryWeapons>(), 2));
            negative = negativeUpgrades;
        }
        return negative;
    }
}
