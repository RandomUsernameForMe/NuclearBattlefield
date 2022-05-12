using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionAnimator : MonoBehaviour
{
    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        LevelManager.OnBattleSceneLoaded += PlayLoadingAnimation;
        LevelManager.OnCampfireSceneLoaded += PlayLoadingAnimation;
    }

    void PlayLoadingAnimation()
    {
        anim.SetTrigger("LevelLoad");
    }

    public void PlayEndLevelAnimation()
    {
        anim.SetTrigger("LevelEnd");
    } 
}
