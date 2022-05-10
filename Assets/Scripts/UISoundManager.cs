using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundManager : MonoBehaviour
{
    public static UISoundManager SMUI;

    [SerializeField]
    private AudioSource bgmMusicSource, uiAudioSource;

    [SerializeField]
    private AudioClip mainMenuMusic;

    [SerializeField]
    private AudioClip selectWhite, selectRed, selectBlue, selectYellow;

    [SerializeField]
    private AudioClip buttonClick;

    private void OnEnable()
    {
        if (UISoundManager.SMUI == null)
        {
            UISoundManager.SMUI = this;
        }
    }

    public void PlayMainMenuBGM()
    {
        bgmMusicSource.clip = mainMenuMusic;
        bgmMusicSource.Play();
    }
    public void PlayButtonClick()
    {
        uiAudioSource.clip = buttonClick;
        uiAudioSource.Play();
    }

    public void PlayCharacterSelect(int index)
    {
        switch (index)
        {
            case 0:
                uiAudioSource.clip = selectWhite;
                break;
            case 1:
                uiAudioSource.clip = selectRed;
                break;
            case 2:
                uiAudioSource.clip = selectBlue;
                break;
            case 3:
                uiAudioSource.clip = selectYellow;
                break;
            default:
                break;
        }
        uiAudioSource.Play();
    }

    public void StopAll()
    {
        uiAudioSource.Stop();
        bgmMusicSource.Stop();
    }
}
