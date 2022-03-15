using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherScoreThree : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Text>().text = GameSetup.GS.otherPlayerScore[2].ToString();
    }
}
