using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController MC;

    public GameObject menuUIOne;
    public GameObject menuUITwo;
    public GameObject menuUIThree; 
    public GameObject startButton;
    public GameObject ControlUIOne;
    public GameObject CreditUIOne;

    public GameObject changeNameButton;
    public GameObject openCreateRoomButton;
    public GameObject createRoomPanel;

    public bool isReady;
    public int playersIndex;
    public int[] isChecked;

    public Text readyPlayers; 

    private void OnEnable()
    {
        if (MenuController.MC == null)
        {
            MenuController.MC = this; 
        }
    }

    private void Start()
    {
        isReady = false;
        isChecked = new int [4] { 0, 0, 0, 0 };
    }

    private void Update()
    {
        RefreshReadyPlayers(); 
    }

    void RefreshReadyPlayers()
    {
        int readys = 0; 
        for (int i = 0; i < 4; i++)
        {
            if (isChecked[i] == 1)
            {
                readys++; 
            }
        }
        readyPlayers.text = readys.ToString(); 
    }

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

            PhotonRoom.room.ChangeListColor(PhotonRoom.room.myNumberInRoom - 1, whichCharacter); 
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

    public void OpenControlMenu()
    {
        menuUIOne.SetActive(false);
        ControlUIOne.SetActive(true);
    }
    public void OpenCreditMenu()
    {
        menuUIOne.SetActive(false);
        CreditUIOne.SetActive(true);
    }
    //Control Menu Buttons
    public void CloseControlMenu()
    {
        ControlUIOne.SetActive(false);
        menuUIOne.SetActive(true);        
    }
    //Credit Menu Buttons
    public void CloseCreditMenu()
    {
        CreditUIOne.SetActive(false);
        menuUIOne.SetActive(true);        
    }
}
