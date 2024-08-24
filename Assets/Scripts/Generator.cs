using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject[] Ground;
    public GameObject[] JumpWall;
    public GameObject Wall;
    public Camera mainCamera;
    public int BoxCount = 4;
    public int JumpWallCount = 2;
    void Start()
    {
        for (int i = 0; i < BoxCount; i++)
        {
            Vector2 pos = Random.insideUnitCircle * 7;
            GameObject selected = Ground[Random.Range(0, Ground.Length)];
            GameObject box = Instantiate(selected, pos, Quaternion.identity);
            Ground[i] = box;
        }
        float screenRight = mainCamera.ViewportToWorldPoint(new Vector2(1, 0)).x;
        float screenLeft = mainCamera.ViewportToWorldPoint(new Vector2(0, 0)).x;
        Instantiate(Wall,new Vector2(screenLeft,0),Quaternion.identity);    
        Instantiate(Wall, new Vector2(screenRight, 0), Quaternion.identity);
        Wall.transform.localScale = new Vector2(5f, 100f);
        for (int i = 0;i < JumpWallCount; i++)
        {

        }
        
    }

    void Update()
    {
        
    }
}
