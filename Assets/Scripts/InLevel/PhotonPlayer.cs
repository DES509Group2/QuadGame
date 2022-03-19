using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{
    public PhotonView PV;
    public GameObject myAvatar;
    public int myTeam;

    private bool firstSpawn; 

    void Start()
    {
        firstSpawn = true; 
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            PV.RPC("RPC_GetTeam", RpcTarget.MasterClient); 
        }
    }

    void Update()
    {
        if (firstSpawn && myAvatar == null && myTeam != 0)
        {
            if (myTeam == 1)
            {
                int spawnPicker = Random.Range(0, GameSetup.GS.spawnPointsTeamOne.Length);
                if (PV.IsMine)
                {
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonNetworkPrefabs", "Player"),
                        GameSetup.GS.spawnPointsTeamOne[spawnPicker].position, GameSetup.GS.spawnPointsTeamOne[spawnPicker].rotation, 0);
                    firstSpawn = false; 
                }
            }
            if (myTeam == 2)
            {
                int spawnPicker = Random.Range(0, GameSetup.GS.spawnPointsTeamTwo.Length);
                if (PV.IsMine)
                {
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonNetworkPrefabs", "Player"),
                        GameSetup.GS.spawnPointsTeamTwo[spawnPicker].position, GameSetup.GS.spawnPointsTeamTwo[spawnPicker].rotation, 0);
                    firstSpawn = false; 
                }
            }
            if (myTeam == 3)
            {
                int spawnPicker = Random.Range(0, GameSetup.GS.spawnPointsTeamThree.Length);
                if (PV.IsMine)
                {
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonNetworkPrefabs", "Player"),
                        GameSetup.GS.spawnPointsTeamThree[spawnPicker].position, GameSetup.GS.spawnPointsTeamThree[spawnPicker].rotation, 0);
                    firstSpawn = false; 
                }
            }
            if (myTeam == 4)
            {
                int spawnPicker = Random.Range(0, GameSetup.GS.spawnPointsTeamFour.Length);
                if (PV.IsMine)
                {
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonNetworkPrefabs", "Player"),
                        GameSetup.GS.spawnPointsTeamFour[spawnPicker].position, GameSetup.GS.spawnPointsTeamFour[spawnPicker].rotation, 0);
                    firstSpawn = false; 
                }
            }
        }
    }

    [PunRPC]
    void RPC_GetTeam()
    {
        myTeam = GameSetup.GS.nextPlayersTeam;
        GameSetup.GS.UpdateTeam();
        PV.RPC("RPC_SentTeam", RpcTarget.OthersBuffered, myTeam); 
    }

    [PunRPC]
    void RPC_SentTeam(int whichTeam)
    {
        myTeam = whichTeam; 
    }
}
