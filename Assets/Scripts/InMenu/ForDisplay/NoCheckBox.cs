using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoCheckBox : MonoBehaviour
{
    [SerializeField]
    private GameObject CheckBox;

    public int playersIndex; 

    private void Start()
    {
        playersIndex = PhotonRoom.room.ListIndex;
        PhotonRoom.room.ListIndex++; 
    }

    private void Update()
    {
        if (MenuController.MC.isChecked[playersIndex] == 1)
        {
            CheckBox.SetActive(true);
            gameObject.SetActive(false);  
        }
    }

    public void OnClickNoCheck()
    {
        if (playersIndex == MenuController.MC.playersIndex)
        {
            CheckBox.SetActive(true);
            MenuController.MC.isReady = true;
            PhotonRoom.room.PV.RPC("RPC_CheckBoxTrue", RpcTarget.AllBuffered, playersIndex);
            // MenuController.MC.isChecked[playersIndex] = 1; 
            gameObject.SetActive(false);
        }
    }

}
