using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeWithCondition
{
    public Upgrade Upgrade { get; }
    public CreatureCondition Condition { get; }

    public UpgradeWithCondition(CreatureCondition cond, Upgrade upg)
    {
        Condition = cond;
        Upgrade = upg;
    }

    public bool IsConditionPassed(Creature creature)
    {
        Condition.creature = creature;
        return Condition.isPassed();
    }

    public bool TryApplyUpgrade(Creature creature, bool positive)
    {
        return Upgrade.TryUpgrade(creature,positive);
    }
}
