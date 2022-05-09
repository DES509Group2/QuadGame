using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject doorOpen;
    public GameObject doorClose;

    private void Update()
    {
        if (GameSetup.GS.groupScore >= GameSetup.GS.winScore)
        {
            doorClose.SetActive(false); 
            doorOpen.SetActive(true);
        }
        else
        {
            doorOpen.SetActive(false);
            doorClose.SetActive(true); 
        }
    }
}
