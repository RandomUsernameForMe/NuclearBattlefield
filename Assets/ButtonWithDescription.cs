using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWithDescription : MonoBehaviour
{
    public Text buttonTextComponent;
    public string buttonText;
    public Text descriptionTextComponent;
    public string descriptionText;

    void Start()
    {
        buttonTextComponent.text = buttonText;
        descriptionTextComponent.text = descriptionText;        
    }

    public void OnExit()
    {
        transform.Find("DescriptionPanel").gameObject.SetActive(false);
    }

    public void OnEnter()
    {
        transform.Find("DescriptionPanel").gameObject.SetActive(true);
    }
}
