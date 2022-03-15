using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherScoreTwo : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Text>().text = GameSetup.GS.otherPlayerScore[1].ToString();
    }
}
