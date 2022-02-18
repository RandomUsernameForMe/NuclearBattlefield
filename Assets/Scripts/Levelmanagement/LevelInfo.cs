using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour
{
    public int currLevel = 0;
    public int maxUpgPointsGain = 10;
    public int currUpgPoints = 0;
    public int upgPointsGain = 0;
    public bool campfire = false;
    public Theatre theatre;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        ResetPointsGain();
    }


    public void ResetPointsGain()
    {
        upgPointsGain = maxUpgPointsGain;
    }


}
