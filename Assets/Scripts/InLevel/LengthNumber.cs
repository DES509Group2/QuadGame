using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LengthNumber : MonoBehaviour
{

    private void Update()
    {
        GetComponent<Text>().text = GameSetup.GS.maxPlayerLength.ToString(); 
    }
}
