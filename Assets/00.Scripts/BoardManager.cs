using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public Cell cellPrefabs;
    public Transform cellParent;

    public Cell[,] map;

    public int mapSize;

    void Start()
    {
        map = new Cell[mapSize,mapSize];

        for(int i = 0; i < mapSize; i++)
        {
            for(int j = 0 ; j< mapSize; j++)
            {
                map[i,j] = Instantiate(cellPrefabs);
                map[i,j].type = (CELL_TYPE)Random.Range(0, 4);
                map[i,j].transform.SetParent(cellParent);
                map[i,j].transform.localPosition = new Vector2(j * 70, -(i * 70));
            }
        }

        ShowMap();
    }

    private void ShowMap()
    {
        for(int i = 0; i < mapSize; i++)
        {
            for(int j = 0 ; j< mapSize; j++)
            {
                if(map[i,j].type == CELL_TYPE.EMPTY)
                    map[i,j].img.color = Color.white;
                else if(map[i,j].type == CELL_TYPE.O)
                    map[i,j].img.color = Color.black;
                else if(map[i,j].type == CELL_TYPE.X)
                    map[i,j].img.color = Color.red;
                else if(map[i,j].type == CELL_TYPE.DK)
                    map[i,j].img.color = Color.grey;
            }
        }
    }
}
