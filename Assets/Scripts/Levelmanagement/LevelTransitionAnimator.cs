using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionAnimator : MonoBehaviour
{
    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        LevelManager.OnBattleLoaded += PlayLoadingAnimation;
        LevelManager.OnCampfireLoaded += PlayLoadingAnimation;
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
