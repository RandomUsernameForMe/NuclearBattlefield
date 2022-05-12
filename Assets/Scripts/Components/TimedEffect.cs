using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedEffect : Component
{
    public int timer;
    public abstract Query Tick();

}
