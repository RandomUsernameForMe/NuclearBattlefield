using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Builder
{
   public double value;
   public abstract void Build(GameObject obj);
}
