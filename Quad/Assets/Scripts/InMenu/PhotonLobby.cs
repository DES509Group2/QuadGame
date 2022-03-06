using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public static PhotonLobby lobby;

    public GameObject startButton; 
    public GameObject menuUITwo;
    public GameObject menuUIThree; 
    public GameObject lobbyButton; 

    public string nickName; 
    public string roomName;
    public int maxRoomSize; 
    public GameObject roomListingPrefab;
    public Transform roomsPanel;

    public List<RoomInfo> roomListings;

    private void Awake()
    {
        lobby = this; 
    }

    void Start()
    {
        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings();
        else
        {
            PhotonNetwork.AutomaticallySyncScene = true; 
        }
        roomListings = new List<RoomInfo>(); 
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Player has connected to the Photon master server");

        PhotonNetwork.AutomaticallySyncScene = true;
        startButton.SetActive(true); 
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        Debug.Log("OnRoomListUpdate . . . ");

        int tempIndex;
        foreach (RoomInfo room in roomList)
        {
            if (roomListings != null)
            {
                tempIndex = roomListings.FindIndex(ByName(room.Name));
            }
            else
            {
                tempIndex = -1; 
            }
            if (tempIndex != -1)
            {
                roomListings.RemoveAt(tempIndex);
                Destroy(roomsPanel.GetChild(tempIndex).gameObject); 
            }
            else
            {
                roomListings.Add(room);
                ListRoom(room); 
            }
        }
    }

    static System.Predicate<RoomInfo> ByName(string name)
    {
        return delegate (RoomInfo room)
        {
            return room.Name == name; 
        };
    }

    void ListRoom(RoomInfo room)
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomsPanel);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.roomName = room.Name;
            tempButton.roomSize = room.PlayerCount;
            tempButton.SetRoom(); 
        }
    }

    public void CreateRoom()
    {
        Debug.Log(PhotonNetwork.NickName + ": Tring to create a new Room");

        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)maxRoomSize };
        PhotonNetwork.CreateRoom(roomName, roomOps); 
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("Tried to creat a new room but failed, there must already be a room with the same name");

    }

    public void OnNickNameChanged(string nameIn)
    {
        nickName = nameIn; 
    }

    public void OnRoomNameChanged(string nameIn)
    {
        roomName = nameIn; 
    }

    public void JoinLobbyOnClick()
    {
        Debug.Log("Player " + PhotonNetwork.NickName + " in lobby");

        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby(); 
        }
    }

    public void OnLobbyButtonClicked()
    {
        menuUITwo.SetActive(false);
        menuUIThree.SetActive(true);
        PhotonNetwork.NickName = nickName;
        Debug.Log("Player: " + PhotonNetwork.NickName); 
    }

}
