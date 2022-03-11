using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;

    public Text lengthDisplay;
    public Text scoreDisplay; 

    public int nextPlayersTeam; 
    public Transform[] spawnPointsTeamOne;
    public Transform[] spawnPointsTeamTwo;
    public Transform[] spawnPointsTeamThree;
    public Transform[] spawnPointsTeamFour;

    public int playerLength;
    public int playerScore; 

    public GameObject GameEndUIOne;
    public GameObject GameEndUITwo;
    public GameObject GameEndUIThree;
    public GameObject GameWinUIOne;
    public GameObject GameWinUITwo;
    public GameObject GameWinUIThree;
    public GameObject GameEndUI;
    public GameObject GameWinUI; 

    private void OnEnable()
    {
        if (GameSetup.GS == null)
        {
            GameSetup.GS = this; 
        }
    }

    private void Start()
    {
        playerLength = 1;
        playerScore = 0; 
    }

    private void Update()
    {
        CheckGameFailed();
        RefreshScore(); 
    }

    void RefreshScore()
    {
        scoreDisplay.text = playerScore.ToString(); 
    }

    public void OnClickEndOneToTwo()
    {
        GameEndUIOne.SetActive(false);
        GameEndUITwo.SetActive(true); 
    }
    public void OnClickEndTwoToThree()
    {
        GameEndUITwo.SetActive(false);
        GameEndUIThree.SetActive(true); 
    }
    public void OnClickEndThreeToRestart()
    {
        DisconnectPlayer(); 
    }
    public void OnClickWinOneToTwo()
    {
        GameWinUIOne.SetActive(false);
        GameWinUITwo.SetActive(true); 
    }
    public void OnClickWinTwoToThree()
    {
        GameWinUITwo.SetActive(false);
        GameWinUIThree.SetActive(true); 
    }
    public void OnClickWinThreeToNextLevel()
    {
        // ++++++++++++++++++++++
    }

    void CheckGameFailed()
    {
        if (playerLength <= 0)
        {
            GameEndUI.SetActive(true); 
        }
    }

    public void DisconnectPlayer()
    {
        Destroy(PhotonRoom.room.gameObject);
        StartCoroutine(DisconnectAndLoad()); 
    }

    IEnumerator DisconnectAndLoad()
    {
        // PhotonNetwork.Disconnect(); 
        PhotonNetwork.LeaveRoom();
        // while (PhotonNetwork.IsConnected)
        while (PhotonNetwork.InRoom)
            yield return null;
        PhotonNetwork.AutomaticallySyncScene = false;
        SceneManager.LoadScene(0); 
    }

    public void UpdateTeam()
    {
        if (nextPlayersTeam < 4)
        {
            nextPlayersTeam++; 
        }
        else
        {
            nextPlayersTeam = 1;
        }
    }

}
