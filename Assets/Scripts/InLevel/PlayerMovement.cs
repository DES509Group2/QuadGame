using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 处理玩家移动 
public class PlayerMovement : MonoBehaviour
{
    private PhotonView PV;
    private AvatarSetup avatarSetup;
    private SpriteRenderer sr; 

    public float step; 
    private float dirX, dirY;
    private Vector3 lastHeadPos;
    private Vector3 nextTailPos; 
    public List<Transform> tailsList;
    private GameObject newTail;

    public float moveInterval;
    public float turnInterval;
    private float turnTimer;
    private bool isTurn;

    public Transform upCheck;
    public Transform downCheck;
    public Transform leftCheck;
    public Transform rightCheck;
    public LayerMask wall;
    private bool inCollision;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        avatarSetup = GetComponent<AvatarSetup>();
        sr = GetComponent<SpriteRenderer>(); 

        avatarSetup.playerLength = 1; 
        SetRandomDirection();
        tailsList = new List<Transform>();
        TailGrow();

        turnTimer = moveInterval; 
    }

    void Update()
    {
        if (!PV.IsMine) return;

        CheckFail(); 
        CheckInCollision(); 

        if (isTurn)
            sr.color = Color.green;
        else
            sr.color = Color.white; 

        GetInput();
        TurnTimer(); 
    }

    void CheckFail()
    {
        if (avatarSetup.playerLength <= 0)
        {
            // ++++++++++
        }
    }

    void CheckInCollision()
    {
        if (dirX > 0 && dirY == 0)
        {
            // Right 
            inCollision = Physics2D.OverlapCircle(rightCheck.position, 0.2f, wall);
        }
        else if (dirX < 0 && dirY == 0)
        {
            // Left
            inCollision = Physics2D.OverlapCircle(leftCheck.position, 0.2f, wall);
        }
        else if (dirX == 0 && dirY > 0)
        {
            // Up
            inCollision = Physics2D.OverlapCircle(upCheck.position, 0.2f, wall); 
        }
        else if (dirX == 0 && dirY < 0)
        {
            // Down 
            inCollision = Physics2D.OverlapCircle(downCheck.position, 0.2f, wall); 
        }
    }

    void TurnTimer()
    {
        turnTimer -= Time.deltaTime; 
        if (turnTimer < 0)
        {
            isTurn = false;
            turnTimer = moveInterval;

            BasicMovement(); 
        }
        else if (turnTimer < turnInterval)
        {
            isTurn = true; 
        }
    }

    void SetRandomDirection()
    {
        int randomDir = Random.Range(0, 4);
        if (randomDir == 0)
        {
            SetDirectionUp(); 
        }
        if (randomDir == 1)
        {
            SetDirectionDown();
        }
        if (randomDir == 2)
        {
            SetDirectionLeft();
        }
        if (randomDir == 3)
        {
            SetDirectionRight(); 
        }
    }
    void SetDirectionUp()
    {
        dirX = 0;
        dirY = step; 
    }
    void SetDirectionDown()
    {
        dirX = 0;
        dirY = -step; 
    }
    void SetDirectionLeft()
    {
        dirX = -step;
        dirY = 0; 
    }
    void SetDirectionRight()
    {
        dirX = step;
        dirY = 0; 
    }

    void GetInput()
    {
        if (!isTurn) return; 

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && dirY != -step)
        {
            SetDirectionUp();
            TailGrow(); 
            isTurn = false;
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && dirX != step)
        {
            SetDirectionLeft();
            TailGrow();
            isTurn = false;
        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && dirY != step)
        {
            SetDirectionDown(); 
            TailGrow();
            isTurn = false;
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && dirX != -step)
        {
            SetDirectionRight(); 
            TailGrow();
            isTurn = false; 
        }
    }

    void BasicMovement()
    {
        if (inCollision) 
        {
            avatarSetup.playerLength--;
            if (avatarSetup.playerLength > 0)
            {
                // PV.RPC("RPC_DestoryTail", RpcTarget.All);
                PhotonNetwork.Destroy(tailsList[tailsList.Count - 1].gameObject); 
                tailsList.Remove(tailsList[tailsList.Count - 1]);
            }
            return; 
        } 

        lastHeadPos = transform.localPosition;
        TailsMove();
        transform.localPosition = new Vector3(lastHeadPos.x + dirX, lastHeadPos.y + dirY, lastHeadPos.z);
    }

    void TailsMove()
    {
        if (tailsList.Count < 1)
            return;
        else
        {
            nextTailPos = tailsList[tailsList.Count - 1].localPosition;  
            for (int i = tailsList.Count - 2; i >=0; i--)
            {
                tailsList[i + 1].localPosition = tailsList[i].localPosition; 
            }
            tailsList[0].localPosition = lastHeadPos; 
        }
    }

    void TailGrow()
    {
        if (PV.IsMine)
        {
            newTail = PhotonNetwork.Instantiate(Path.Combine("PhotonNetworkPrefabs", "Tail"), nextTailPos, Quaternion.identity, 0); 
            avatarSetup.playerLength++; 
        }
        if (newTail)
            tailsList.Add(newTail.transform);
    }
}
