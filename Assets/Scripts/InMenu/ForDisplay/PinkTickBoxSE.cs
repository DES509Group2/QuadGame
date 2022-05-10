using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkTickBoxSE : MonoBehaviour
{
    public GameObject OpenAudio;
    public GameObject MuteAudio;
    public GameObject PinkTick;

    private bool isMuteSE;

    private void Start()
    {
        // isMuteSE = false;
        if (PlayerInfo.PI.isMuteSE == 0)
        {
            isMuteSE = false;
        }
        else
        {
            isMuteSE = true; 
        }
    }

    public void OnClickPinkTick()
    {
        if (isMuteSE)
        {
            MuteAudio.SetActive(false);
            OpenAudio.SetActive(true);
            PinkTick.SetActive(true);
            isMuteSE = false;
            PlayerInfo.PI.isMuteSE = 0;
        }
        else
        {
            MuteAudio.SetActive(true);
            OpenAudio.SetActive(false);
            PinkTick.SetActive(false);
            isMuteSE = true;
            PlayerInfo.PI.isMuteSE = 1; 
        }
    }
}
