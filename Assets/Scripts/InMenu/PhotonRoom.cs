using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    // Room Info
    public static PhotonRoom room;
    public PhotonView PV; 

    public bool isGameLoaded;
    public int currenScene;

    // Player Info
    Player[] photonPlayers;
    public int playersInRoom;
    public int myNumberInRoom;

    public GameObject menuUIThree;
    public GameObject menuUIFour;
    public Transform playersPanel;
    public GameObject playerListingPrefab;
    public GameObject playButton;

    // public bool isReady;
    public List<GameObject> playersList; 
    public GameObject readyButton;

    public int ListIndex;

    public Text totalPlayers; 

    private void Awake()
    {
        // set up singleton
        if (PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        }
        else
        {
            if (PhotonRoom.room != this)
            {
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this; 
            }
        }
        DontDestroyOnLoad(this.gameObject); 
    }

    public override void OnEnable()
    {
        // subscribe to functions
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading; 
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading; 
    }

    void Start()
    {
        // set private variable
        PV = GetComponent<PhotonView>();

        // isReady = false; 
    }

    private void Update()
    {
        if (MenuController.MC.isReady)
        {
            readyButton.SetActive(false);
        }
        else
        {
            readyButton.SetActive(true); 
        }
    }

    public override void OnJoinedRoom()
    {
        // sets playe data when we join the room
        base.OnJoinedRoom();
        Debug.Log("We are now in a room");

        menuUIThree.SetActive(false);
        menuUIFour.SetActive(true); 
        if (PhotonNetwork.IsMasterClient)
        {
            playButton.SetActive(true); 
        }

        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;

        MenuController.MC.playersIndex = myNumberInRoom - 1; 

        ClearPlayerListings();
        ListPlayers();

    }

    void ClearPlayerListings()
    {
        for (int i = playersPanel.childCount - 1; i >= 0; i--)
        {
            Destroy(playersPanel.GetChild(i).gameObject); 
        }

        playersList.Clear(); 
    }

    void ListPlayers()
    {
        if (PhotonNetwork.InRoom)
        {
            ListIndex = 0;
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                GameObject tempListing = Instantiate(playerListingPrefab, playersPanel);
                Text tempText = tempListing.transform.GetChild(0).GetComponent<Text>();
                tempText.text = player.NickName;
                playersList.Add(tempListing);
            }
            ChangeListColor(myNumberInRoom - 1, PlayerInfo.PI.mySelectedCharacter);
            totalPlayers.text = PhotonNetwork.PlayerList.Length.ToString(); 
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("A new player has joined the room");

        ClearPlayerListings();
        ListPlayers();

        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom++;  
    }

    public void StartGame()
    {
        isGameLoaded = true;
        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.LoadLevel(1); 
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        // called when multiplayer scene is loaded
        currenScene = scene.buildIndex; 
        if (currenScene == 1)
        {
            isGameLoaded = true;
            RPC_CreatePlayer(); 
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonNetworkPrefabs", "PhotonNetworkPlayer"), transform.position, Quaternion.identity, 0); 
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log(otherPlayer.NickName + " has left the game");
        playersInRoom--;

        ClearPlayerListings();
        ListPlayers(); 
    }

    public void ChangeListColor(int playerIndex, int roleIndex)
    {
        PV.RPC("RPC_ChangeListColor", RpcTarget.All, playerIndex, roleIndex);
    } 

    [PunRPC]
    void RPC_ChangeListColor(int playerIndex, int roleIndex)
    {
        ChangePlayersListColor(playerIndex, roleIndex); 
    }

    void ChangePlayersListColor(int playerIndex, int roleIndex)
    {
        switch (roleIndex)
        {
            case 0:
                playersList[playerIndex].GetComponent<Image>().color = Color.white;
                break;
            case 1:
                playersList[playerIndex].GetComponent<Image>().color = Color.red;
                break;
            case 2:
                playersList[playerIndex].GetComponent<Image>().color = Color.blue;
                break;
            case 3:
                playersList[playerIndex].GetComponent<Image>().color = Color.yellow;
                break;
            default:
                break;
        }
    }

    [PunRPC]
    void RPC_CheckBoxFalse(int index)
    {
        MenuController.MC.isChecked[index] = 0;
    }

    [PunRPC]
    void RPC_CheckBoxTrue(int index)
    {
        MenuController.MC.isChecked[index] = 1;
    }

}
