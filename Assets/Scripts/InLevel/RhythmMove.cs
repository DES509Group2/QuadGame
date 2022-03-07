using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMove : MonoBehaviour
{
    private float startPositionX;
    private float startPositionY;
    //AudioSource audioData;
    [SerializeField]
    private float shotInterval;
    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        startPositionY = transform.localPosition.y;
        initialSetting();
        //audioData = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(transform.localPosition.x == startPositionX)
        {
            isMoving = false;
        }
        else if(transform.localPosition.x == 0)
        {
            //audioData.Play(0);
            transform.LeanSetLocalPosX(startPositionX);
            isMoving = false;


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
            transform.LeanMoveLocal(new Vector2(0, startPositionY), shotInterval);
            isMoving = true;
        }
  

    }


}
