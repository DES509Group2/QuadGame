using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WArrow : MonoBehaviour
{

    void Update()
    {
        if (PlayerInfo.PI.mySelectedCharacter == 0)
        {
            GetComponent<Image>().enabled = true;
        }
        else
        {
            GetComponent<Image>().enabled = false; 
        }
    }
}
