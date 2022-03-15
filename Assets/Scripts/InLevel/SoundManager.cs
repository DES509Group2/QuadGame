using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager SM; 

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioSource supportAudioSounce; 
    [SerializeField]
    private AudioClip beatPlayer1, beatPlayer2, beatPlayer3, beatPlayer4;
    [SerializeField]
    private AudioClip tailIncrease, tailDecrease;

    private void OnEnable()
    {
        if (SoundManager.SM == null)
        {
            SoundManager.SM = this;
        }
    }
    public void PlayBeatOne()
    {
        audioSource.clip = beatPlayer1; 
        audioSource.Play(); 
    }
    public void PlayBeatTwo()
    {
        audioSource.clip = beatPlayer2; 
        audioSource.Play();
    }
    public void PlayBeatThree()
    {
        audioSource.clip = beatPlayer3; 
        audioSource.Play();
    }
    public void PlayBeatFour()
    {
        audioSource.clip = beatPlayer4; 
        audioSource.Play();
    }

    public void PlayTailIncrease()
    {
        supportAudioSounce.clip = tailIncrease;
        supportAudioSounce.Play(); 
    }
    
    public void PlayTailDecrease()
    {
        supportAudioSounce.clip = tailDecrease;
        supportAudioSounce.Play();  
    }

}
