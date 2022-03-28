using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbsSpawn : MonoBehaviour
{
    [SerializeField]
    private Vector2 TopLeftPosition; //自定义地图四个角位置
    [SerializeField]
    private Vector2 TopRightPosition;
    [SerializeField]
    private Vector2 BottomLeftPosition;
    [SerializeField]
    private Vector2 BottomRightPosition;

    //orbs生成坐标
    private float orbsX;
    private float orbsY;
    //prefab
    public GameObject OrbsWhite;
    public GameObject OrbsRed;
    public GameObject OrbsYellow;
    public GameObject OrbsBlue;
    //确定四个象限
    private int x1;
    private int y1;
    [SerializeField]
    private int SpawnCount = 2;
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> orbsList = new List<GameObject>() { OrbsWhite, OrbsRed, OrbsYellow, OrbsBlue};

        //确定四个象限；
        x1 = (int)(TopLeftPosition.x + TopRightPosition.x) / 2;
        y1 = (int)(TopLeftPosition.y + BottomLeftPosition.y) / 2;
        //根据地图边界确定生成位置

        //生成左上象限orbs
        for(int i = 1; i <= SpawnCount; i++)
        {
            for(int j = 0; j <= 3; j++)
            {
                orbsX = Random.Range((int)TopLeftPosition.x, x1 - 1)+ 0.5f;
                orbsY = Random.Range((int)TopLeftPosition.y, y1 + 1)- 0.5f;
                Instantiate(orbsList[j], new Vector2(orbsX, orbsY), Quaternion.identity);
            }
            for (int j = 0; j <= 3; j++)
            {
                orbsX = Random.Range((int)BottomLeftPosition.x, x1 - 1) + 0.5f;
                orbsY = Random.Range((int)BottomLeftPosition.y, y1 - 1) + 0.5f;
                Instantiate(orbsList[j], new Vector2(orbsX, orbsY), Quaternion.identity);
            }
            for (int j = 0; j <= 3; j++)
            {
                orbsX = Random.Range((int)TopRightPosition.x, x1 + 1) - 0.5f;
                orbsY = Random.Range((int)TopRightPosition.y, y1 + 1) - 0.5f;
                Instantiate(orbsList[j], new Vector2(orbsX, orbsY), Quaternion.identity);
            }
            for (int j = 0; j <= 3; j++)
            {
                orbsX = Random.Range((int)BottomRightPosition.x, x1 + 1) - 0.5f;
                orbsY = Random.Range((int)BottomRightPosition.y, y1 - 1) + 0.5f;
                Instantiate(orbsList[j], new Vector2(orbsX, orbsY), Quaternion.identity);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
