using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuUIOne;
    public GameObject menuUITwo;
    public GameObject startButton;

    public void OnClickCharacterPick(int whichCharacter)
    {
        if (PlayerInfo.PI != null)
        {
            PlayerInfo.PI.mySelectedCharacter = whichCharacter;
            PlayerPrefs.SetInt("MyCharacter", whichCharacter); 
        }
    }

    public void OnStartButtonClicked()
    {
        startButton.SetActive(false);
        menuUIOne.SetActive(false);
        menuUITwo.SetActive(true); 
    }


}
