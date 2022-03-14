using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelNumber : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Text>().text = GameSetup.GS.levelIndex.ToString();  
    }
}
