using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winscript : MonoBehaviour
{
    public GameObject obj;
    public string str;

    public void Update()
    {
        if (Input.GetKeyDown(str))
        {
            winLevel();
        }
    }
    // Start is called before the first frame update
    public void winLevel()
    {
        obj.SetActive(true);
    }
}
