using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalNumber : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Text>().text = GameSetup.GS.GetSurvivalTime(); 
    }
}
