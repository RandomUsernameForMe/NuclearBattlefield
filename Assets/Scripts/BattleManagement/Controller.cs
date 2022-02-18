using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Controller : MonoBehaviour
{
    public abstract void Activate(Creature creature);
}
