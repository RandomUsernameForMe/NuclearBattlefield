using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAura : MonoBehaviour
{
    MeshRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>();
    }

    public MeshRenderer GetRenderer() 
    {
        return render;
    }
}
