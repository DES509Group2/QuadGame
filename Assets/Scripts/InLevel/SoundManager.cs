using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager SM; 

    [SerializeField]
    private AudioSource audioSource, bgmAudioSource, bgmMusicSource; 
    [SerializeField]
    private AudioSource supportAudioSounce, supportAudioSounceLoop;  

    [SerializeField]
    private AudioClip metronome, Level1Bgm;
    [SerializeField]
    private AudioClip tailIncreaseWhite, tailIncreaseRed, tailIncreaseBlue, tailIncreaseYellow;
    [SerializeField]
    private AudioClip tailDecreaseWhite, tailDecreaseRed, tailDecreaseBlue, tailDecreaseYellow;
    [SerializeField]
    private AudioClip pickupWhite, pickupRed, pickupBlue, pickupYellow;

    [SerializeField]
    private AudioClip deathWhite, deathRed, deathBlue, deathYellow;

    [SerializeField]
    private AudioClip doorUnlock;

    private bool isStop; 

    private void OnEnable()
    {
        if (SoundManager.SM == null)
        {
            SoundManager.SM = this;
        }

        isStop = false; 
    }

    public void PlayMetronome()
    {
        if (isStop == true) return; 

        bgmAudioSource.clip = metronome;
        bgmAudioSource.Play();
        bgmMusicSource.clip = Level1Bgm;
        bgmMusicSource.Play();
    }

    public void PauseMetronome()
    {
        if (isStop == true) return;

        bgmAudioSource.clip = metronome;
        bgmAudioSource.Pause();
        bgmMusicSource.clip = Level1Bgm;
        bgmMusicSource.Pause();
    }
    public void PlayTailIncrease(int index)
    {
        if (isStop == true) return;

        switch (index)
        {
            case 0:
                audioSource.clip = tailIncreaseWhite;
                break;
            case 1:
                audioSource.clip = tailIncreaseRed;
                break;
            case 2:
                audioSource.clip = tailIncreaseBlue; 
                break;
            case 3:
                audioSource.clip = tailIncreaseYellow; 
                break;
            default:
                break;
        }
        audioSource.Play(); 
    }
    
    public void PlayTailDecrease(int index)
    {
        if (isStop == true) return;

        switch (index)
        {
            case 0:
                audioSource.clip = tailDecreaseWhite;
                break;
            case 1:
                audioSource.clip = tailDecreaseRed;
                break;
            case 2:
                audioSource.clip = tailDecreaseBlue;
                break;
            case 3:
                audioSource.clip = tailDecreaseYellow;
                break;
            default:
                break;
        }
        audioSource.Play();
    }

    public void PlayPickup(int index)
    {
        if (isStop == true) return;

        switch (index)
        {
            case 0:
                supportAudioSounce.clip = pickupWhite;
                break;
            case 1:
                supportAudioSounce.clip = pickupRed;
                break;
            case 2:
                supportAudioSounce.clip = pickupBlue;
                break;
            case 3:
                supportAudioSounce.clip = pickupYellow;
                break;
            default:
                break;
        }
        supportAudioSounce.Play(); 
    }


    public void PlayDeath(int index)
    {
        if (isStop == true) return;

        switch (index)
        {
            case 0:
                supportAudioSounce.clip = deathWhite;
                break;
            case 1:
                supportAudioSounce.clip = deathRed;
                break;
            case 2:
                supportAudioSounce.clip = deathBlue;
                break;
            case 3:
                supportAudioSounce.clip = deathYellow;
                break;
            default:
                break;
        }
        supportAudioSounce.Play();
        // To reference in function: SoundManager.SM.PlayDeath(playerIndex); 
    }

    public void StopAll()
    {
        audioSource.Stop();
        bgmAudioSource.Stop();
        bgmMusicSource.Stop();
        supportAudioSounce.Stop();
        supportAudioSounceLoop.Stop();
        isStop = true; 
    }

    public void PlayDoorUnlocked()
    {
        if (isStop == false)
        {
            supportAudioSounce.clip = doorUnlock;
            supportAudioSounce.Play();
        }
    }
}
