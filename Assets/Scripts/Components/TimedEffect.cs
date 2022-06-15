using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedEffect : Component
{
    public int timer;
    public bool active;
    public abstract Query Tick();

}
