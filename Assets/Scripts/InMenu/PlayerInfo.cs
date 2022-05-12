// ����洢��ѡ������Ԥ���弰��ѡ���Ԥ����ƫ��

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo PI;

    public int mySelectedCharacter;
    public int isMuteBM; 
    public int isMuteSE; 

    public GameObject[] allCharacters;
    public GameObject[] allTails; 

    private void OnEnable()
    {
        if (PlayerInfo.PI == null)
        {
            PlayerInfo.PI = this;
        }
        else
        {
            if (PlayerInfo.PI != this)
            {
                Destroy(PlayerInfo.PI.gameObject);
                PlayerInfo.PI = this; 
            }
        }
        DontDestroyOnLoad(this.gameObject); 
    }

    void Start()
    {
        // PlayerPrefs ��ֵ�־ô����ڱ���
        if (PlayerPrefs.HasKey("MyCharacter"))
        {
            mySelectedCharacter = PlayerPrefs.GetInt("MyCharacter");
        }
        else
        {
            mySelectedCharacter = 0;
            PlayerPrefs.SetInt("MyCharacter", mySelectedCharacter); 
        }

        if(PlayerPrefs.HasKey("IsMuteBM"))
        {
            isMuteBM = PlayerPrefs.GetInt("IsMuteBM");
        }
        else
        {
            isMuteBM = 0;
            PlayerPrefs.SetInt("IsMuteBM", isMuteBM); 
        }

        if(PlayerPrefs.HasKey("IsMuteSE"))
        {
            isMuteSE = PlayerPrefs.GetInt("IsMuteSE"); 
        }
        else
        {
            isMuteSE = 0;
            PlayerPrefs.SetInt("IsMuteSE", isMuteSE); 
        }
    }

}
