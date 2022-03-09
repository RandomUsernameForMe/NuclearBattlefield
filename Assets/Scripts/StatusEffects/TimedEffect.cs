using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedEffect : StatusEffect
{
    public int timer;
    public abstract Query Tick();

}
