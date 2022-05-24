using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradableComponent : Component
{
    public abstract bool TryUpgrade(bool positive);
}
