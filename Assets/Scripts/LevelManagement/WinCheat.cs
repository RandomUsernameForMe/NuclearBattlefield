using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCheat : MonoBehaviour
{
    public GameObject obj;
    public string str;

    /// <summary>
    /// Cheating intended for debugging
    /// </summary>
    public void Update()
    {
        if (Input.GetKeyDown(str))
        {
            WinLevelByCheat();
        }
    }

    public void WinLevelByCheat()
    {
        obj.SetActive(true);
    }
}
