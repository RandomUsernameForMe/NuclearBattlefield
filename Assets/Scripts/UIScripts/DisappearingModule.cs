using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingModule : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    public void MouseEnter()
    {
        obj.SetActive(true);
    }

    public void MouseExit()
    {
        obj.SetActive(false);
    }
}
