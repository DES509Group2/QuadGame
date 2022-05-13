using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SoundManager.SM.PlayButtonClick();
            PauseMenuUI.SetActive(true);
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PauseMetronome();
            Time.timeScale = 0;
        }


    }

    public void gameResume()
    {
        SoundManager.SM.PlayButtonClick();
        PauseMenuUI.SetActive(false);
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayMetronome();
        Time.timeScale = 1;
    }
    public void restartGame()
    {
        SoundManager.SM.PlayButtonClick();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void closeRoom()
    {
        SoundManager.SM.PlayButtonClick();
        SceneManager.LoadScene("Menu");
    }


}
