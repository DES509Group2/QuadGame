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

    public int failedPlayers; 
    public int wonPlayers;

    public bool isWhiteWin;
    public bool isRedWin;
    public bool isBlueWin;
    public bool isYellowWin; 

    public GameObject deathPanel;

    public bool isEnd;

    public Animator anim;
    public Animator anim1;
    public Animator anim2;

    public GameObject EndScreenUI;
    private bool isEndMusicPlaying;

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

        failedPlayers = 0;

        StartCoroutine(startAnim());
        Time.timeScale = 0;

        wonPlayers = 0;

        isEnd = false;
        isEndMusicPlaying = false;

        isWhiteWin = false;
        isRedWin = false;
        isBlueWin = false;
        isYellowWin = false; 
    }

    private void Update()
    {
        if (isTimeFly)
            survivalTime += Time.deltaTime;

        if (playerLength <= 0 && !isEnd)
        {
            deathPanel.SetActive(true); 
        }
        
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
        if (failedPlayers == PhotonNetwork.PlayerList.Length) 
        {
            // GameEndUI.SetActive(true);
            EndScreenUI.SetActive(true); 
            SoundManager.SM.StopAll();
            if (isEndMusicPlaying == false)
            {
                SoundManager.SM.PlayFailEndScreenMusic();
                isEndMusicPlaying = true;
            }
            isTimeFly = false;

            deathPanel.SetActive(false); 
        }
    }

    public void CheckGameWin() 
    {
        if (failedPlayers + wonPlayers == PhotonNetwork.PlayerList.Length && groupScore >= winScore)
        {
            // GameWinUI.SetActive(true);
            EndScreenUI.SetActive(true); 
            SoundManager.SM.StopAll();

            if (isEndMusicPlaying == false)
            {
                SoundManager.SM.PlayWinEndScreenMusic();
                isEndMusicPlaying = true;
            }
            //
            isTimeFly = false;

            deathPanel.SetActive(false); 
        }
    }

    public void DisconnectPlayer()
    {
        SoundManager.SM.PlayButtonClick();
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

    IEnumerator startAnim()
    {
        //yield return null;
        //Debug.Log("1");

        anim = GameObject.Find("1").GetComponent<Animator>();
        //anim.Play("1", 0);
        yield return new WaitForSecondsRealtime(1.0f);
        GameObject animContro = GameObject.Find("StartAnim");
        //print(animContro);
        animContro.transform.Find("2").gameObject.SetActive(true);
        //GameObject number1 = animContro.transform.Find("1").gameObject;
        //number1.SetActive(true);
        //GameObject.Find("StartAnim").gameObject.transform.Find("1").gameObject.SetActive(true);

        //anim1.Play("1", 0);
        yield return new WaitForSecondsRealtime(1.0f);
        animContro.transform.Find("3").gameObject.SetActive(true);
        //anim2.Play("1", 0);
        yield return new WaitForSecondsRealtime(1.0f);
        animContro.transform.Find("go").gameObject.SetActive(true);
        Time.timeScale = 1;
    }

}
