using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassStars : MonoBehaviour
{
    public GameObject FullStarRed;
    public GameObject FullStarBlue;
    public GameObject FullStarYellow;
    public GameObject FullStarWhite;

    private void OnEnable()
    {
        if (GameSetup.GS.isRedWin)
            FullStarRed.SetActive(true); 
        else
            FullStarRed.SetActive(false);

        if (GameSetup.GS.isBlueWin)
            FullStarBlue.SetActive(true);
        else
            FullStarBlue.SetActive(false);

        if (GameSetup.GS.isYellowWin)
            FullStarYellow.SetActive(true);
        else
            FullStarYellow.SetActive(false);

        if (GameSetup.GS.isWhiteWin)
            FullStarWhite.SetActive(true);
        else
            FullStarWhite.SetActive(false); 
    }
}
