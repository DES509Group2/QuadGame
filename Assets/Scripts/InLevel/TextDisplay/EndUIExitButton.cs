using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUIExitButton : MonoBehaviour
{

    public void OnClickExit()
    {
        GameSetup.GS.DisconnectPlayer(); 
    }
}
