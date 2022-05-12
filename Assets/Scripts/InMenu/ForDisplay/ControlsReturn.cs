using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsReturn : MonoBehaviour
{
    public GameObject ControlsPage; 

    public void OnclickMainMenu()
    {
        ControlsPage.SetActive(false); 
    }
}
