using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control of round shape below creatures used for signaling targetting
/// </summary>
public class RingHighlighter : MonoBehaviour
{
    bool active = false;
    public GameObject canvas;
    public Color _default;
    public Color highlight;
    public Color hover;
    public Color playing;
    public float emissionIntensity;
    public Renderer render;

    void Start()
    {        
        canvas.SetActive(false);
    }


    public void Reset() {
        ColorAura colorAura = GetComponentInChildren<ColorAura>();
        render = colorAura.GetComponentInChildren<Renderer>();
        render.material.SetColor("_EmissionColor", _default * emissionIntensity);
        active = false;
    }

    public void SetToHighlightColor() {
        active = true;
        render.material.SetColor("_EmissionColor", highlight * emissionIntensity);
    } 

    public void SetToActivityColor()
    {
        render.material.SetColor("_EmissionColor", playing * emissionIntensity);
    }

    void OnMouseUp() {
        
        if (active) {
            var targeter = GameObject.Find("BattleManager").GetComponent<PlayerController>();
            targeter.PlayActionAfterTargetAquired(GetComponentInParent<Creature>());
            Reset();
        }
        active = false;
    }

    void OnMouseOver() {

        if (active) {            
            render.material.SetColor("_EmissionColor", hover*emissionIntensity);
        }
        GetComponent<CreatureStatsDescriptionPanel>().UpdateUI();
        canvas.SetActive(true);
    }

    void OnMouseExit() {
        if (active) {            
            render.material.SetColor("_EmissionColor", highlight * emissionIntensity);
        }
        canvas.SetActive(false);
    }

    
}
