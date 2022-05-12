using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyerCount : MonoBehaviour
{
    GameObject lgt;
    GameObject cam;
    public int count = 0;

    private void OnEnable()
    {
        LevelManager.OnBattleSceneLoaded += Refresh;
    }

    private void OnDisable()
    {
        LevelManager.OnBattleSceneLoaded -= Refresh;
    }
    public void Refresh()
    {
        cam = GameObject.Find("Main Camera");
        lgt = GameObject.Find("StrobeLight");
        switch (count)
        {
            case 1:
                cam.GetComponentInChildren<Animator>().SetBool("CameraShake1", true);
                break;
            case 2:
                lgt.transform.GetChild(0).gameObject.SetActive(true);
                cam.GetComponentInChildren<Animator>().SetBool("CameraShake1", false);
                cam.GetComponentInChildren<Animator>().SetBool("CameraShake2", true);
                lgt.GetComponent<Animation>().Play();
                break;
            case 3:
                SceneManager.LoadScene(3);
                break;
        }
    }
    public void CountUp()
    {
        count += 1;
        Refresh();
    }
}
