using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMove : MonoBehaviour
{
    private float startPositionX;
    AudioSource audioData;
    [SerializeField]
    private float shotInterval;
    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        initialSetting();
        audioData = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(transform.localPosition.x == startPositionX)
        {
            isMoving = false;
        }
        else if(transform.localPosition.x == 0)
        {
            transform.LeanSetLocalPosX(startPositionX);
            isMoving = false;
            audioData.Play(0);

        }
        else
        {
            isMoving = true;
        }
        blockMove();
    }

    private void initialSetting()
    {
        if(transform.localPosition.x < 0)
        {
            startPositionX = transform.localPosition.x;
        }
        else if(transform.localPosition.x > 0)
        {
            startPositionX = transform.localPosition.x;
        }
    }

    public void blockMove()
    {
        if(isMoving == false)
        {
            transform.LeanMoveLocal(new Vector2(0, -148), shotInterval);
            isMoving = true;
        }
  

    }


}
