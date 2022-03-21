using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    [SerializeField]
    private GameObject noCheckBox;

    public int playersIndex;

    private void OnEnable()
    {
        playersIndex = noCheckBox.GetComponent<NoCheckBox>().playersIndex; 
    }

    private void Update()
    {
        if (MenuController.MC.isChecked[playersIndex] == 0)
        {
            noCheckBox.SetActive(true);
            gameObject.SetActive(false); 
        }
    }

    public void OnClickCheck()
    {
        if (playersIndex == MenuController.MC.playersIndex)
        {
            noCheckBox.SetActive(true);
            MenuController.MC.isReady = false;
            PhotonRoom.room.PV.RPC("RPC_CheckBoxFalse", RpcTarget.All, playersIndex);
            // MenuController.MC.isChecked[playersIndex] = 0; 
            gameObject.SetActive(false);
        }
    }

}
