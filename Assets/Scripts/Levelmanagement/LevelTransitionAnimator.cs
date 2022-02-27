using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionAnimator : MonoBehaviour
{
    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        LevelManager.OnBattleLoaded += LoadAnimation;
        LevelManager.OnCampfireLoaded += LoadAnimation;
    }

    void LoadAnimation()
    {
        anim.SetTrigger("LevelLoad");
    }

    public void EndLevelAnimation()
    {
        anim.SetTrigger("LevelEnd");
    } 
}
