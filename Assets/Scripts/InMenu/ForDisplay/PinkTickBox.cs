using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkTickBox : MonoBehaviour
{
    public GameObject OpenAudio;
    public GameObject MuteAudio;
    public GameObject PinkTick;

    private bool isMute;

    private void Start()
    {
        isMute = false; 
    }

    public void OnClickPinkTick()
    {
        if (isMute)
        {
            MuteAudio.SetActive(false);
            OpenAudio.SetActive(true);
            PinkTick.SetActive(true);
            isMute = false; 
        }
        else
        {
            MuteAudio.SetActive(true);
            OpenAudio.SetActive(false);
            PinkTick.SetActive(false);
            isMute = true; 
        }
    }
}
