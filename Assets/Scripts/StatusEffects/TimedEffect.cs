using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedEffect : Module
{
    public int timer;
    public abstract Action Tick();

}
