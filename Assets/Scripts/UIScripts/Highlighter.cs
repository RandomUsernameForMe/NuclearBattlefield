using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    bool active = false;
    TargetingSystem targeter;

    public GameObject canvas;

    public Color def;
    public Color highlight;
    public Color hover;
    public Color playing;
    public float emissionIntensity;

    public Renderer render;

    void Start()
    {
        targeter = GameObject.Find("BattleManager").GetComponent<TargetingSystem>();
        canvas.SetActive(false);
    }


    public void Reset() {
        ColorAura colorAura = GetComponentInChildren<ColorAura>();
        render = colorAura.GetComponentInChildren<Renderer>();
        render.material.SetColor("_EmissionColor", def * emissionIntensity);
        active = false;
    }

    public void Activate() {
        active = true;
        render.material.SetColor("_EmissionColor", highlight * emissionIntensity);
    } 

    public void SetToPlayingColor()
    {
        render.material.SetColor("_EmissionColor", playing * emissionIntensity);
    }

    void OnMouseUp() {
        
        if (active) {
            targeter.TargetAquired(GetComponentInParent<Creature>());
            Reset();
        }
        active = false;
    }

    void OnMouseOver() {

        if (active) {            
            render.material.SetColor("_EmissionColor", hover*emissionIntensity);
        }
        GetComponent<CreatureStatus>().UpdateUI();
        canvas.SetActive(true);
    }

    void OnMouseExit() {
        if (active) {            
            render.material.SetColor("_EmissionColor", highlight * emissionIntensity);
        }
        canvas.SetActive(false);
    }

    
}
