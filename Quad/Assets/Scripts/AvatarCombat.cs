using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarCombat : MonoBehaviour
{
    private PhotonView PV;
    private AvatarSetup avatarSetup;

    public Text lengthDisplay; 

    void Start()
    {
        PV = GetComponent<PhotonView>();
        avatarSetup = GetComponent<AvatarSetup>();
        lengthDisplay = GameSetup.GS.lengthDisplay; 
    }

    void Update()
    {
        if (!PV.IsMine)
        {
            return; 
        }

        lengthDisplay.text = avatarSetup.playerLength.ToString(); 
    }
}
