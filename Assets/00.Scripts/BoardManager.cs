using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public Image cellPrefabs;
    public Transform cellParent;

    public int mapSize;

    void Start()
    {
        for(int i = 0; i < mapSize; i++)
        {
            for(int j = 0 ; j< mapSize; j++)
            {
                Image newObj = Instantiate(cellPrefabs);
                newObj.transform.SetParent(cellParent);
                newObj.transform.localPosition = new Vector2(j * 70, -(i * 70));
                // newObj.color = flag  ? Color.white : Color.black;
            }
        }
    }
}
