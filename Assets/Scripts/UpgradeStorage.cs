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
            positiveUpgrades.Add(new UpgradeWithCondition(new ComponentCondition<Health>(), new ComponentUpgrade<Health>()));
            positiveUpgrades[0].Upgrade.buttonText = "Raise Health";
            positiveUpgrades[0].Upgrade.cost = 2;
            positiveUpgrades[0].Upgrade.descriptionText = "Raises health by 5, costs " + positiveUpgrades[0].Upgrade.cost + " points.";
            positiveUpgrades.Add(new UpgradeWithCondition(new ComponentCondition<Armored>(), new ComponentUpgrade<Armored>()));
            positiveUpgrades[1].Upgrade.buttonText = "Armor up";
            positiveUpgrades[1].Upgrade.cost = 2;
            positiveUpgrades[1].Upgrade.descriptionText = "You reduce physical damage by 3 more, costs " + positiveUpgrades[1].Upgrade.cost + " points.";
            positiveUpgrades.Add(new UpgradeWithCondition(new ComponentCondition<PhysicalWeapon>(), new ComponentUpgrade<PhysicalWeapon>()));
            positiveUpgrades[2].Upgrade.buttonText = "Sharpen weapon";
            positiveUpgrades[2].Upgrade.cost = 2;
            positiveUpgrades[2].Upgrade.descriptionText = "Your weapon will deal 5 more physical damage, costs " + positiveUpgrades[2].Upgrade.cost + " points.";
            positiveUpgrades.Add(new UpgradeWithCondition(new ComponentCondition<PoisonBlast>(), new ComponentUpgrade<PoisonBlast>()));
            positiveUpgrades[3].Upgrade.buttonText = "Brew better poison";
            positiveUpgrades[3].Upgrade.cost = 2;
            positiveUpgrades[3].Upgrade.descriptionText = "Increases strength or suration of your poison, costs " + positiveUpgrades[3].Upgrade.cost + " points.";
            positiveUpgrades.Add(new UpgradeWithCondition(new ComponentCondition<HealingWave>(), new ComponentUpgrade<HealingWave>()));
            positiveUpgrades[4].Upgrade.buttonText = "Improve Healing";
            positiveUpgrades[4].Upgrade.cost = 2;
            positiveUpgrades[4].Upgrade.descriptionText = "You heal for 10 more health, costs " + positiveUpgrades[4].Upgrade.cost + " points.";
            positiveUpgrades.Add(new UpgradeWithCondition(new ComponentCondition<Armored>("",false), new ComponentCreator<Armored>()));
            positiveUpgrades[5].Upgrade.buttonText = "Wear Armor";
            positiveUpgrades[5].Upgrade.cost = 2;
            positiveUpgrades[5].Upgrade.descriptionText = "You start wearing armor, costs " + positiveUpgrades[5].Upgrade.cost + " points.";
            positive = positiveUpgrades;
        }
        return positive;
    }

    public static List<UpgradeWithCondition> GetNegativeUpgrades()
    {

        if (negative == null)
        {
            var negativeUpgrades = new List<UpgradeWithCondition>();
            negativeUpgrades.Add(new UpgradeWithCondition(new ComponentCondition<Health>(), new ComponentUpgrade<Health>()));
            negativeUpgrades[0].Upgrade.cost = 2;
            negativeUpgrades.Add(new UpgradeWithCondition(new ComponentCondition<Armored>(), new ComponentUpgrade<Armored>()));
            negativeUpgrades[1].Upgrade.cost = 2;
            negativeUpgrades.Add(new UpgradeWithCondition(new ComponentCondition<PhysicalWeapon>(), new ComponentUpgrade<PhysicalWeapon>()));
            negativeUpgrades[2].Upgrade.cost = 2;
            negative = negativeUpgrades;
        }
        return negative;
    }
}
