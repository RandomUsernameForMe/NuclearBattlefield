using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHolder : MonoBehaviour
{
    public Upgrade upg;
    public Text txt;

    public void Start()
    {
        transform.Find("DescriptionPanel").gameObject.SetActive(false);
    }

    public void OnExit()
    {
        transform.Find("DescriptionPanel").gameObject.SetActive(false);
    }
    
    public void OnEnter()
    {
        txt.text = upg.descriptionText;
        transform.Find("DescriptionPanel").gameObject.SetActive(true);
    }
}
