using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// StatusBuilder is an object used for creating a StatusEffect within target creature
/// because its not possible to transfer MonoBehaviors between Unity GameObjects.
/// </summary>
public abstract class ComponentBuilder
{
   public double value;
   public abstract void BuildStatusEffect(GameObject obj);
}
