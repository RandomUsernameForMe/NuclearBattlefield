using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    AudioSource aud;
    float currentTime = 0;
    float duration = 2;
    bool fade = false;

    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Handles volume fading in and out between scenes
    /// </summary>
    void Update()
    {
        if (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            aud.volume = Mathf.Lerp(0, 0.05f, currentTime / duration);
        }

        if (fade)
        {
            aud.volume = Mathf.Lerp(0.3f, 0, (Time.time - currentTime) / duration);
        }

    }

    internal void Fade(float transitionTime)
    {
        currentTime = Time.time;
        duration = transitionTime;
        fade = true;
    }
}
