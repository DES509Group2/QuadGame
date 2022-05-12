using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButton : MonoBehaviour
{
    public PhotonView PV;

    public bool isRedOk;
    public bool isBlueOk;
    public bool isYellowOk;
    public bool isWhiteOk;

    public int numberOk;

    public GameObject RedPoint;
    public GameObject BluePoint;
    public GameObject YellowPoint;
    public GameObject WhitePoint;
    public GameObject RedTick;
    public GameObject BlueTick;
    public GameObject YellowTick;
    public GameObject WhiteTick; 

    private void OnEnable()
    {
        PV = GetComponent<PhotonView>();
        isRedOk = false;
        isBlueOk = false;
        isYellowOk = false;
        isWhiteOk = false;
        numberOk = 0;  
    }

    public void OnclickRetry()
    {
        PV.RPC("SomeOneReady", RpcTarget.All, PlayerInfo.PI.mySelectedCharacter);
    }

    [PunRPC]
    public void SomeOneReady(int playerIndex)
    {
        switch (playerIndex)
        {
            case 0:
                if (!isWhiteOk) numberOk++;
                isWhiteOk = true;
                WhitePoint.SetActive(true);
                WhiteTick.SetActive(true); 
                break;
            case 1:
                if (!isRedOk) numberOk++;
                isRedOk = true;
                RedPoint.SetActive(true);
                RedTick.SetActive(true); 
                break;
            case 2:
                if (!isBlueOk) numberOk++;
                BluePoint.SetActive(true);
                BlueTick.SetActive(true); 
                isBlueOk = true;
                break;
            case 3:
                if (!isYellowOk) numberOk++;
                YellowPoint.SetActive(true);
                YellowTick.SetActive(true); 
                isYellowOk = true;
                break;
            default:
                break;
        }
        if (numberOk == 4)
        {
            PhotonRoom.room.StartGame(); 
        }
    }
}
