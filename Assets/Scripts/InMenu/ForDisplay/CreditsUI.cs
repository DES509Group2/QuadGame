using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsUI : MonoBehaviour
{
    public GameObject creditsUI;

    public void OnClickOpenCredits()
    {
        creditsUI.SetActive(true); 
    }

    public void OnClickCloseCredits()
    {
        creditsUI.SetActive(false); 
    }
}
