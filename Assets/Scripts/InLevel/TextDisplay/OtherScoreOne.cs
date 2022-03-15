using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherScoreOne : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Text>().text = GameSetup.GS.otherPlayerScore[0].ToString(); 
    }
}
