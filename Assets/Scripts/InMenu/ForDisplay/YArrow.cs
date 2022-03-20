using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YArrow : MonoBehaviour
{

    void Update()
    {
        if (PlayerInfo.PI.mySelectedCharacter == 3)
        {
            GetComponent<Image>().enabled = true; 
        }
        else
        {
            GetComponent<Image>().enabled = false; 
        }
    }
}
