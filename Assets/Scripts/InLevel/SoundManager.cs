using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager SM; 

    [SerializeField]
    private AudioSource audioSource;
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
        audioSource.clip = tailIncrease;
        audioSource.Play(); 
    }
    
    public void PlayTailDecrease()
    {
        audioSource.clip = tailDecrease;
        audioSource.Play(); 
    }

}
