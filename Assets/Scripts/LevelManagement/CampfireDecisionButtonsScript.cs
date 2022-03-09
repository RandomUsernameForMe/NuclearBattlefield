using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CampfireDecisionButtonsScript : MonoBehaviour
{
    public GameObject descriptionRepeatLevelButton;
    public GameObject descriptionNextLevelButton;

    // Start is called before the first frame update
    public void Start()
    {
        descriptionRepeatLevelButton.SetActive(false);
        descriptionNextLevelButton.SetActive(false);
    }

    public void RepeatOnExit()
    {
        descriptionRepeatLevelButton.SetActive(false);
    }

    public void RepeatOnEnter()
    {
        descriptionRepeatLevelButton.SetActive(true);
    }

    public void RepeatOnPress()
    {
        var manager = GameObject.Find("LevelInfo").GetComponent<LevelManager>();
        manager.RepeatLevel();
    }

    public void NextOnExit()
    {
        descriptionNextLevelButton.SetActive(false);
    }

    public void NextOnEnter()
    {
        descriptionNextLevelButton.SetActive(true);
    }

    public void NextOnPress()
    {
        var manager = GameObject.Find("LevelInfo").GetComponent<LevelManager>();
        manager.NextLevel();
    }
}
