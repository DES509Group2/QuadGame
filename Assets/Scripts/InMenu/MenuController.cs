using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuUIOne;
    public GameObject menuUITwo;
    public GameObject menuUIThree; 
    public GameObject startButton;

    public GameObject changeNameButton;
    public GameObject openCreateRoomButton;
    public GameObject createRoomPanel; 
    
    public void OnClickChangeName()
    {
        menuUIThree.SetActive(false); 
        menuUITwo.SetActive(true); 
    }

    public void OpenRoomPanel()
    {
        createRoomPanel.SetActive(true); 
    }

    public void CloseRoomPanel()
    {
        createRoomPanel.SetActive(false); 
    }

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

    public void OnExitButtonClicked()
    {
        Application.Quit(); 
    }
}
