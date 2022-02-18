using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonsScript : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public void Start()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
    }
    public void RepeatOnExit()
    {
        panel1.SetActive(false);
    }
    public void RepeatOnEnter()
    {
        panel1.SetActive(true);
    }

    public void RepeatOnPress()
    {
        var manager = GameObject.Find("LevelInfo").GetComponent<LevelManager>();
        manager.RepeatLevel();
    }

    public void NextOnExit()
    {
        panel2.SetActive(false);
    }
    public void NextOnEnter()
    {
        panel2.SetActive(true);
    }

    public void NextOnPress()
    {
        var manager = GameObject.Find("LevelInfo").GetComponent<LevelManager>();
        manager.NextLevel();
    }
}
