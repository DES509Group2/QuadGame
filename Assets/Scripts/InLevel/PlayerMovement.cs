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
    public LayerMask player; 
    private bool inCollision;

    private AudioSource audioData;
    public delegate void ScaleChangeAciton();
    public static event ScaleChangeAciton scaleChange;

    [SerializeField]
    private Vector2 TopLeftPosition; //自定义地图四个角位置
    [SerializeField]
    private Vector2 TopRightPosition;
    [SerializeField]
    private Vector2 BottomLeftPosition;
    [SerializeField]
    private Vector2 BottomRightPosition;
    private int x1;
    private int y1;

    [SerializeField] 
    private int playerIndex;

    private int pressCounter;

    // private bool isEnd;

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

        audioData = GetComponent<AudioSource>();

        if (PV.IsMine)
        {
            Debug.Log("+++++++++" + PlayerInfo.PI.mySelectedCharacter + "+++++++++"); 
            playerIndex = PlayerInfo.PI.mySelectedCharacter; 
        }

        pressCounter = 0;

        SoundManager.SM.PlayMetronome(); 
    }

    void Update()
    {
        if (!PV.IsMine) return;

        if (!GameSetup.GS.isEnd)
        {
            CheckInCollision();
        }
        else
        {
            inCollision = true; 
        }

        if (isTurn)
            sr.color = Color.green;
        else
            sr.color = Color.white; 

        GetInput();
        TurnTimer(); 
    }

    void CheckInCollision()
    {
        if (dirX > 0 && dirY == 0)
        {
            // Right 
            inCollision = Physics2D.OverlapCircle(rightCheck.position, 0.2f, wall) || Physics2D.OverlapCircle(rightCheck.position, 0.2f, player);
        }
        else if (dirX < 0 && dirY == 0)
        {
            // Left
            inCollision = Physics2D.OverlapCircle(leftCheck.position, 0.2f, wall) || Physics2D.OverlapCircle(leftCheck.position, 0.2f, player);
        }
        else if (dirX == 0 && dirY > 0)
        {
            // Up
            inCollision = Physics2D.OverlapCircle(upCheck.position, 0.2f, wall) || Physics2D.OverlapCircle(upCheck.position, 0.2f, player);
        }
        else if (dirX == 0 && dirY < 0)
        {
            // Down 
            inCollision = Physics2D.OverlapCircle(downCheck.position, 0.2f, wall) || Physics2D.OverlapCircle(downCheck.position, 0.2f, player);
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

            pressCounter = 0; 
        }
        else if (turnTimer < turnInterval)
        {
            isTurn = true; 
        }
    }

    void SetRandomDirection()
    {
        //确定四个象限；
        x1 = (int)(TopLeftPosition.x + TopRightPosition.x) / 2;
        y1 = (int)(TopLeftPosition.y + BottomLeftPosition.y) / 2;
        if(transform.position.x < x1 && transform.position.y > y1)
        {
            SetDirectionDown();
        }
        if (transform.position.x > x1 && transform.position.y > y1)
        {
            SetDirectionLeft();
        }
        if (transform.position.x > x1 && transform.position.y < y1)
        {
            SetDirectionUp();
        }
        if (transform.position.x < x1 && transform.position.y < y1)
        {
            SetDirectionRight();
        }
        //int randomDir = Random.Range(0, 4);
        //if (randomDir == 0)
        //{
        //    SetDirectionUp(); 
        //}
        //if (randomDir == 1)
        //{
        //    SetDirectionDown();
        //}
        //if (randomDir == 2)
        //{
        //    SetDirectionLeft();
        //}
        //if (randomDir == 3)
        //{
        //    SetDirectionRight(); 
        //}
    }
    void SetDirectionUp()
    {
        dirX = 0;
        dirY = step;
        PV.RPC("RPC_HeadArrowChange", RpcTarget.All, 1); 
    }
    void SetDirectionDown()
    {
        dirX = 0;
        dirY = -step;
        PV.RPC("RPC_HeadArrowChange", RpcTarget.All, 2);
    }
    void SetDirectionLeft()
    {
        dirX = -step;
        dirY = 0;
        PV.RPC("RPC_HeadArrowChange", RpcTarget.All, 3);
    }
    void SetDirectionRight()
    {
        dirX = step;
        dirY = 0;
        PV.RPC("RPC_HeadArrowChange", RpcTarget.All, 4); 
    }

    [PunRPC]
    void RPC_HeadArrowChange(int dir)
    {
        if (avatarSetup && avatarSetup.myHeadArrow)
        {
            avatarSetup.myHeadArrow.transform.rotation = Quaternion.identity;
            switch (dir)
            {
                case 1:
                    avatarSetup.myHeadArrow.transform.Rotate(0, 0, 0);
                    break;
                case 2:
                    avatarSetup.myHeadArrow.transform.Rotate(0, 0, 180);
                    break;
                case 3:
                    avatarSetup.myHeadArrow.transform.Rotate(0, 0, 90);
                    break;
                case 4:
                    avatarSetup.myHeadArrow.transform.Rotate(0, 0, -90);
                    break;
                default:
                    break;
            }
        }
    }

    void GetInput()
    {
        // if (!isTurn) return;
        if (pressCounter >= 3) return; 

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && dirY != -step)
        {
            pressCounter++;
            if (isTurn)
            {
                SetDirectionUp();
                TailGrow();
                isTurn = false;

                SoundManager.SM.PlayTailIncrease(playerIndex); 

                if (scaleChange != null)
                    scaleChange();
                GameSetup.GS.PlayCombo(); 
            }
            else
            {
                GameSetup.GS.currentCombo = 0; 
            }
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && dirX != step)
        {
            pressCounter++;
            if (isTurn)
            {
                SetDirectionLeft();
                TailGrow();
                isTurn = false;

                SoundManager.SM.PlayTailIncrease(playerIndex);

                if (scaleChange != null)
                    scaleChange();
                GameSetup.GS.PlayCombo(); 
            }
            else
            {
                GameSetup.GS.currentCombo = 0; 
            }
        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && dirY != step)
        {
            pressCounter++;
            if (isTurn)
            {
                SetDirectionDown();
                TailGrow();
                isTurn = false;

                SoundManager.SM.PlayTailIncrease(playerIndex);

                if (scaleChange != null)
                    scaleChange();
                GameSetup.GS.PlayCombo(); 
            }
            else
            {
                GameSetup.GS.currentCombo = 0;
            }
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && dirX != -step)
        {
            pressCounter++;
            if (isTurn)
            {
                SetDirectionRight();
                TailGrow();
                isTurn = false;

                SoundManager.SM.PlayTailIncrease(playerIndex);

                if (scaleChange != null)
                    scaleChange();
                GameSetup.GS.PlayCombo(); 
            }
            else
            {
                GameSetup.GS.currentCombo = 0; 
            }
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
                SoundManager.SM.PlayTailDecrease(playerIndex); 
            }
            else
            {
                GameSetup.GS.playerLength = 0; 
                if (PV.IsMine)
                {
                    if (!GameSetup.GS.isEnd)
                    {
                        SoundManager.SM.PlayDeath(playerIndex);
                        PV.RPC("RPC_AddFailedPlayer", RpcTarget.All);
                    }
                    PhotonNetwork.Destroy(gameObject);
                }
            }
            return; 
        } 

        lastHeadPos = transform.localPosition;
        TailsMove();
        transform.localPosition = new Vector3(lastHeadPos.x + dirX, lastHeadPos.y + dirY, lastHeadPos.z);
    }

    [PunRPC]
    void RPC_AddFailedPlayer()
    {
        GameSetup.GS.failedPlayers++; 
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
        {
            tailsList.Add(newTail.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("picking Orb" + playerIndex);

        if (collision.tag == "Orb" + playerIndex)
        {
            Debug.Log("picked Orb" + playerIndex);

            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false; 
            // Physics2D.IgnoreCollision(collision, GetComponent<CircleCollider2D>()); 

            AddScore();
            SoundManager.SM.PlayPickup(playerIndex); 

            if (PV.IsMine) // Destroy(collision.gameObject);
            {
                PV.RPC("RPC_DestroyOrb", RpcTarget.All, collision.tag, transform.position);
            }
        }

        if (collision.tag == "Door" + playerIndex)
        {
            if (PV.IsMine)
            {
                if (GameSetup.GS.groupScore >= GameSetup.GS.winScore)
                {
                    GameSetup.GS.isEnd = true; 
                    PV.RPC("RPC_AddWonPlayers", RpcTarget.All);
                }
                PV.RPC("RPC_CheckWin", RpcTarget.All); 
            }
        }
    }

    [PunRPC]
    void RPC_CheckWin()
    {
        GameSetup.GS.CheckGameWin(); 
    }

    [PunRPC]
    void RPC_AddWonPlayers()
    {
        GameSetup.GS.wonPlayers++; 
    }

    void AddScore()
    {
        if (PV.IsMine)
        {
            GameSetup.GS.playerScore++;
            int tempIndex = playerIndex; 
            int tempScore = GameSetup.GS.playerScore; 
            PV.RPC("RPC_AllScorePlus", RpcTarget.All, tempIndex, tempScore); 
            PV.RPC("RPC_GroupScorePlus", RpcTarget.All);
        }
    }

    [PunRPC]
    void RPC_AllScorePlus(int index, int score)
    {
        GameSetup.GS.allPlayerScore[index] = score;  
    }

    [PunRPC]
    void RPC_GroupScorePlus()
    {
        GameSetup.GS.groupScore++; 
    }

    [PunRPC]
    void RPC_DestroyOrb(string orbTag, Vector3 pos)
    {
        GameObject[] orbs = GameObject.FindGameObjectsWithTag(orbTag);

        foreach (GameObject orb in orbs)
        {
            if (orb.transform.position.x == pos.x && orb.transform.position.y == pos.y)
            {
                Destroy(orb); 
            }
        }
    }
}
