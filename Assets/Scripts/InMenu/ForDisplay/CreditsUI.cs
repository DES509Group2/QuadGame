using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsUI : MonoBehaviour
{
    public GameObject creditsUI;

    public void OnClickOpenCredits()
    {
        UISoundManager.SMUI.PlayButtonClick();
        creditsUI.SetActive(true); 
    }

    public void OnClickCloseCredits()
    {
        UISoundManager.SMUI.PlayButtonClick();
        creditsUI.SetActive(false); 
    }
}
