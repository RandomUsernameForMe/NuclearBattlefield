using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    public Transform mainCamera;
    // Start is called before the first frame update

    public void OnEnable()
    {
        mainCamera = GameObject.Find("CameraHelper").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion r = mainCamera.rotation;
        Quaternion n = new Quaternion(r.x-1, r.y, r.z, r.w);
    }
}
