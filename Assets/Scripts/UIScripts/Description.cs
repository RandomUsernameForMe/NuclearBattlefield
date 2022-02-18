using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    public Text descriptor;

    void Start() {
        descriptor.gameObject.SetActive(false);
    }

    public void ShowAttackDescription() {
        descriptor.gameObject.SetActive(true);
    }

    public void ShowSpecialDescription() {
        descriptor.gameObject.SetActive(true);
    }

    public void ShowSkipDescription() {        
        descriptor.text = "Skips this creture's action with no benefit";
        descriptor.gameObject.SetActive(true);
    }

    public void HideDescription() {
        descriptor.gameObject.SetActive(false);
    }
}
