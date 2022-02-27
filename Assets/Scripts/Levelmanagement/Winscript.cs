using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winscript : MonoBehaviour
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
            winLevel();
        }
    }

    public void winLevel()
    {
        obj.SetActive(true);
    }
}
