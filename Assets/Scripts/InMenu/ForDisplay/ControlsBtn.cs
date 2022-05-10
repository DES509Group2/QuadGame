using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsBtn : MonoBehaviour
{
    public GameObject ControlsPage; 

    public void OnClickControls()
    {
        UISoundManager.SMUI.PlayButtonClick();
        ControlsPage.SetActive(true); 
    }
}
