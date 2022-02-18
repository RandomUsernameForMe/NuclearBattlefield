using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator anim;
    public LevelTransitionAnimator transAnim;
    public LevelInfo info;

    private float TRANSITIONTIME = 1f;
    private int MAXLEVEL = 3;

    // two delegates that are called at start of appropriate loaded level
    public delegate void BattleSceneLoaded();
    public static event BattleSceneLoaded OnBattleLoaded;

    public delegate void CampfireLoaded();
    public static event CampfireLoaded OnCampfireLoaded;
   

    /// <summary>
    /// Special function for loading 1st Battle. Contains a special animation, and therefore takes longer.
    /// </summary>
    public void LoadLevelNewGame()
    {
        anim.SetTrigger("Level1");
        info.currLevel = 1;
        info.campfire = false;
        LoadLevel(1, 2);
    }

    /// <summary>
    /// In case player choses to repeat the same level for less points, reload the same level.
    /// </summary>
    public void RepeatLevel()
    {
        info.upgPointsGain = info.upgPointsGain - 2;
        info.campfire = false;
        LoadLevel(1, 1);
    }

    /// <summary>
    /// Increments the global variable describing current level and loads a battle with new parameter
    /// </summary>
    public void NextLevel()
    {
        info.ResetPointsGain();
        if (info.currLevel != MAXLEVEL)
        {
            info.currLevel += 1;
        }
        info.campfire = false;
        LoadLevel(1,1);
    }

    /// <summary>
    /// Load the campfire level
    /// </summary>
    internal void LoadCampFire()
    {
        info.campfire = true;
        LoadLevel(2,1);
    }

    /// <summary>
    /// Load a new level and transition between scenes. Transition is timed and ending animation is played. Depending on the folowing level, this evokes event delegates.
    /// </summary>
    /// <param name="i">Scene number: 1 - battle, 2 - campfire</param>
    /// <param name="time">Time in seconds to wait before loading</param>
    public void LoadLevel(int i,float time)
    {
        transAnim.EndLevelAnimation();
        GameObject.Find("Audio Source").GetComponent<AudioControl>().Fade(time);
        StartCoroutine(WaitAndLoad(i,time));
    }

    private IEnumerator WaitAndLoad(int i,float time)
    {
        yield return new WaitForSeconds(time);        
        SceneManager.LoadScene(i);
        if (!info.campfire) {
            OnBattleLoaded();
        }
        else {
            OnCampfireLoaded();
        }
    }
}
