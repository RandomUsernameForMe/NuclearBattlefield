using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireUpgradesStorage : MonoBehaviour
{
    static List<UpgradeWithCondition> upgrades;
    public static List<UpgradeWithCondition> GetCampfireSpecificUpgrades()
    {
        if (upgrades == null)
        {
            var upgrs = new List<UpgradeWithCondition>();
            upgrs.Add(new UpgradeWithCondition(new HasValue(QueryParameter.Dead,1,""), new CampfireRevive()));
            upgrs.Add(new UpgradeWithCondition(new HasValue(QueryParameter.Dead,0, ""), new CampfireFullHeal()));
            upgrs.Add(new UpgradeWithCondition(new ComponentCondition<PowerStrike>(), new DestroyerEquipUpgrade()));
            upgrades = upgrs;
        }
        return upgrades;
    }
}
