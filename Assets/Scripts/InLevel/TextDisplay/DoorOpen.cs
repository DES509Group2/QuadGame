using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject doorOpen;
    public GameObject doorClose;

    private bool isDoorLocked;

    private void Start()
    {
        isDoorLocked = false;
    }

    private void Update()
    {
        if (GameSetup.GS.groupScore >= GameSetup.GS.winScore)
        {
            PlayDoorAudio();
            doorClose.SetActive(false); 
            doorOpen.SetActive(true);
        }
        else
        {
            doorOpen.SetActive(false);
            doorClose.SetActive(true); 
        }
    }

    private void PlayDoorAudio()
    {
        if (isDoorLocked == false)
        {
            SoundManager.SM.PlayDoorUnlocked();
            isDoorLocked = true;
        }
    }

}
