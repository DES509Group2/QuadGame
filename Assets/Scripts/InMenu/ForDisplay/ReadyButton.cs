using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyButton : MonoBehaviour
{

    public void OnClickReady()
    {
        MenuController.MC.isReady = true;
        // MenuController.MC.isChecked[MenuController.MC.playersIndex] = 1; 
        PhotonRoom.room.PV.RPC("RPC_CheckBoxTrue", RpcTarget.AllBuffered, MenuController.MC.playersIndex); 
        gameObject.SetActive(false); 
    }
}
