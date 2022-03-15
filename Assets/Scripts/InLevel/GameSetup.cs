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
    public Text groupScoreDisplay; 

    public int nextPlayersTeam; 
    public Transform[] spawnPointsTeamOne;
    public Transform[] spawnPointsTeamTwo;
    public Transform[] spawnPointsTeamThree;
    public Transform[] spawnPointsTeamFour;

    public int playerLength;
    public int playerScore;
    public int groupScore;
    public int winScore;
    public int levelIndex;
    public int maxPlayerLength;

    public GameObject GameEndUIOne;
    public GameObject GameEndUITwo;
    public GameObject GameEndUIThree;
    public GameObject GameWinUIOne;
    public GameObject GameWinUITwo;
    public GameObject GameWinUIThree;
    public GameObject GameEndUI;
    public GameObject GameWinUI;

    public GameObject ComboUI;
    public int currentCombo;
    public int maxCombo;

    private float survivalTime;
    private bool isTimeFly;

    public int[] allPlayerScore;
    public int[] otherPlayerScore; 

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
        maxPlayerLength = 1;
        playerScore = 0;
        groupScore = 0;
        levelIndex = SceneManager.GetActiveScene().buildIndex;

        currentCombo = 0;
        maxCombo = 0;

        survivalTime = 0;
        isTimeFly = true;

        allPlayerScore = new int [4] { 0, 0, 0, 0 };
        otherPlayerScore = new int[3] { 0, 0, 0 }; 
    }

    private void Update()
    {
        if (isTimeFly)
            survivalTime += Time.deltaTime;
        
        CheckGameFailed();
        RefreshScore();
        RefreshMax(); 
    }

    void RefreshMax()
    {
        if (playerLength > maxPlayerLength)
        {
            maxPlayerLength = playerLength; 
        }

        if (currentCombo > maxCombo)
        {
            maxCombo = currentCombo; 
        }
    }

    void RefreshScore()
    {
        scoreDisplay.text = playerScore.ToString();
        groupScoreDisplay.text = groupScore.ToString();
        RefreshOtherPlayerScore(); 
    }

    void RefreshOtherPlayerScore()
    {
        int tempIndex = PlayerInfo.PI.mySelectedCharacter;
        int j = 0; 
        for (int i = 0; i < 4; i++)
        {
           if (i != tempIndex)
            {
                otherPlayerScore[j] = allPlayerScore[i];
                j++; 
            }
        }
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
            isTimeFly = false;
        }
    }

    public void CheckGameWin() 
    {
        if (groupScore >= winScore)
        {
            GameWinUI.SetActive(true);
            isTimeFly = false;
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

    public void PlayCombo()
    {
        currentCombo++; 
        ComboUI.SetActive(true); 
    }

    public string GetSurvivalTime()
    {
        int hour = (int)survivalTime / 3600;
        int minute = (int)(survivalTime - hour * 3600) / 60;
        int second = (int)(survivalTime - hour * 3600 - minute * 60);
        int millisecond = (int)((survivalTime - (int)survivalTime) * 1000);

        return string.Format("{0:D2}:{1:D2}:{2:D2}", minute, second, millisecond); 
    }

}
