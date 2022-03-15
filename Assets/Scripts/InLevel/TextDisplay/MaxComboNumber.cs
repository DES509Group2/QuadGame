using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxComboNumber : MonoBehaviour
{

    private void Update()
    {
        GetComponent<Text>().text = GameSetup.GS.maxCombo.ToString(); 
    }
}
