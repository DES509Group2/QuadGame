using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BArrow : MonoBehaviour
{

    void Update()
    {
        if (PlayerInfo.PI.mySelectedCharacter == 2)
        {
            GetComponent<Image>().enabled = true; 
        }
        else
        {
            GetComponent<Image>().enabled = false; 
        }
    }
}
