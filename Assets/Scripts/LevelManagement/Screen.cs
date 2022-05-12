using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This describes one screen of the story screens section after every scene transition.
/// </summary>
public class Screen
{
    public Condition cond;
    public string characterName;
    public bool played;
    public Texture2D source;

    public Screen(Texture2D v, Type t, string name)
    {
        cond = new HasStatus(t,name);
        characterName = name;
        source = v;
        played = false;
    }

    public Screen(Texture2D v, QueryParameter i, double val, string name)
    {
        cond = new HasValue(i, val, name);
        source = v;
    }

    public Screen(Texture2D v, int level)
    {
        cond = new IsWhichLevel(level);
        source = v;
    }

    public bool Possible()
    {
        return (!played && cond.isPassed());
    }
}
