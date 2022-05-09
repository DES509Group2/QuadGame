using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailAvatarSetup : MonoBehaviour
{
    private PhotonView PV;
    public GameObject myTail; 

    void Start()
    {
        PV = GetComponent<PhotonView>(); 
        if (PV.IsMine)
        {
            Debug.Log("AddTail"); 
            PV.RPC("RPC_AddTail", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedCharacter);
        }
    }

    [PunRPC]
    void RPC_AddTail(int whichTail)
    {
        myTail = Instantiate(PlayerInfo.PI.allTails[whichTail], transform.position, transform.rotation, transform); 
    }

}
