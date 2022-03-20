using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RArrow : MonoBehaviour
{

    void Update()
    {
        if (PlayerInfo.PI.mySelectedCharacter == 1)
        {
            GetComponent<Image>().enabled = true; 
        }
        else
        {
            GetComponent<Image>().enabled = false; 
        }
    }
}
