using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource aud;
    float currentTime = 0;
    float duration = 2;
    bool fade = false;

    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            aud.volume = Mathf.Lerp(0, 0.3f, currentTime / duration);
        }

        if (fade)
        {
            aud.volume = Mathf.Lerp(0.3f, 0, (Time.time - currentTime) / duration);
        }

    }

    internal void Fade(float transitionTime)
    {
        currentTime = Time.time;
        fade = true;
    }
}
