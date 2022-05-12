using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeWithCondition
{
    CreatureCondition condition;
    GenericUpgrade upgrade;

    public UpgradeWithCondition(CreatureCondition cond, GenericUpgrade upg)
    {
        condition = cond;
        upgrade = upg;
    }

    public bool IsConditionPassed(Creature creature)
    {
        condition.creature = creature;
        return condition.isPassed();
    }

    public void ApplyUpgrade(Creature creature)
    {
        upgrade.Upgrade(creature);
    }
}
