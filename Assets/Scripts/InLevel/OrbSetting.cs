using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSetting : MonoBehaviour
{
    private void OnDestroy()
    {
        GameSetup.GS.playerScore++; 
    }
}
