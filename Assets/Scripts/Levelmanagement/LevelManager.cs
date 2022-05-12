using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Manager present in every scene, responsible for timed transitions in between them
/// </summary>
public class LevelManager : MonoBehaviour
{
    public Animator anim;
    public LevelTransitionAnimator transAnim;
    public LevelInfo info;

    private int CAMPFIRELEVEL = 2;
    private int BATTLELEVEL = 1;
    private int MAXLEVEL = 3;

    // two delegates that are called at start of appropriate loaded level
    public delegate void OnBattleSceneLoadedEvent();
    public static event OnBattleSceneLoadedEvent OnBattleSceneLoaded;

    public delegate void OnCampfireLoadedEvent();
    public static event OnCampfireLoadedEvent OnCampfireSceneLoaded;
   

    /// <summary>
    /// Special function for loading 1st Battle. Contains a special animation.
    /// </summary>
    public void LoadLevelNewGame()
    {
        anim.SetTrigger("Level1");
        info.currLevel = 1;
        info.campfire = false;
        LoadLevel(BATTLELEVEL, 2);
    }

    public void RepeatLevel()
    {
        info.upgPointsGain = info.upgPointsGain - 2;
        info.campfire = false;
        LoadLevel(BATTLELEVEL, 1);
    }

    public void NextLevel()
    {
        info.ResetPointsGain();
        if (info.currLevel != MAXLEVEL)
        {
            info.currLevel += 1;
        }
        info.campfire = false;
        LoadLevel(BATTLELEVEL,1);
    }

    internal void LoadCampFire()
    {
        info.campfire = true;
        LoadLevel(CAMPFIRELEVEL,1);
    }

    /// <summary>
    /// Loads a new level and transition between scenes. Transition is timed and ending animation is played. Depending on the folowing level, this evokes event delegates.
    /// </summary>
    /// <param name="levelNum">Scene number: 1 - battle, 2 - campfire</param>
    /// <param name="time">Time in seconds to wait before loading</param>
    public void LoadLevel(int levelNum,float time)
    {
        transAnim.PlayEndLevelAnimation();
        GameObject.Find("Audio Source").GetComponent<AudioControl>().Fade(time);
        StartCoroutine(WaitAndLoad(levelNum,time));
    }

    private IEnumerator WaitAndLoad(int levelNum,float time)
    {
        yield return new WaitForSeconds(time);        
        SceneManager.LoadScene(levelNum);
    }

    internal static void TriggerBattleLoaded()
    {
        OnBattleSceneLoaded();
    }

    internal static void CampfireTrigger()
    {
        OnCampfireSceneLoaded();
    }
}
