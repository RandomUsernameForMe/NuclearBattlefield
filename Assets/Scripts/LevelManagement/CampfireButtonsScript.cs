using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CampfireButtonsScript : MonoBehaviour
{
    public GameObject descriptionRepeatLevelButton;
    public GameObject descriptionNextLevelButton;

    // Start is called before the first frame update
    public void Start()
    {
        descriptionRepeatLevelButton.SetActive(false);
        descriptionNextLevelButton.SetActive(false);
    }

    public void OnExitRepeatLevelButton()
    {
        descriptionRepeatLevelButton.SetActive(false);
    }

    public void OnEnterRepeatLevelButton()
    {
        descriptionRepeatLevelButton.SetActive(true);
    }

    public void OnPressRepeatLevelButton()
    {
        var manager = GameObject.Find("LevelInfo").GetComponent<LevelManager>();
        manager.RepeatLevel();
    }

    public void OnExitNextLevelButton()
    {
        descriptionNextLevelButton.SetActive(false);
    }

    public void OnEnterNextLevelButton()
    {
        descriptionNextLevelButton.SetActive(true);
    }

    public void OnPressNextLevelButton()
    {
        var manager = GameObject.Find("LevelInfo").GetComponent<LevelManager>();
        manager.LoadNextLevel();
    }
}
