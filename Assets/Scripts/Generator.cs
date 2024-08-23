using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject[] Box;

    public int BoxCount = 4;
    void Start()
    {
        for (int i = 0; i < BoxCount; i++)
        {
            Vector2 pos = Random.insideUnitCircle * 7;
            GameObject selected = Box[Random.Range(0, Box.Length)];
            GameObject box = Instantiate(selected, pos, selected.transform.rotation);
            box.transform.rotation = Quaternion.identity;
        }
    }

    void Update()
    {
        
    }
}
