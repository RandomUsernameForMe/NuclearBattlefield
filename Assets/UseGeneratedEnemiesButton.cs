using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseGeneratedEnemiesButton : MonoBehaviour
{
    public Party nextEnemyGroup;

    private void Start()
    {
        gameObject.GetComponent<Button>().interactable = false;
    }
    public void LoadNextLevel()
    {
        var obj = GameObject.Find("LevelInfo");
        var levelInfo = obj.GetComponent<LevelInfo>();
        if (levelInfo.controlGroup)
        {
            levelInfo.currLevel++;
            obj.GetComponent<PartyHolder>().party = obj.GetComponent<ManMadeGroups>().GetParty(levelInfo.currLevel);
        }
        else
        {
            obj.GetComponent<PartyHolder>().party = nextEnemyGroup;            
        }
        obj.GetComponent<LevelManager>().LoadNextLevel();


    }
}
