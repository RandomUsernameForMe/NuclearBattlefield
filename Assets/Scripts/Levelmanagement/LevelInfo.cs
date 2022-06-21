using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Holds game state that needs to be passed in between scenes.
/// </summary>
public class LevelInfo : MonoBehaviour
{
    public bool controlGroup = false;
    public int currLevel = 0;
    public int maxUpgPointsGain = 10;
    public int currUpgPoints = 0;
    public int upgPointsGain = 0;
    public bool isAtCampfire = false;
    public StoryScreens storyScreens;

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
