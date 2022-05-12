using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLoaderTrigger : MonoBehaviour
{
    void Start()
    {
        LevelManager.TriggerBattleLoaded();
    }
}
