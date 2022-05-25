using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public static BoardManager ins;

    public Cell cellPrefabs;
    public Transform cellParent;

    public Cell[,] board;

    public int mapSize;

    private void Awake()
    {
        if (ins == null)
            ins = this;
    }

    void Start()
    {
        board = new Cell[mapSize, mapSize];

        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                board[y,x] = Instantiate(cellPrefabs);
                board[y,x].type = (CELL_TYPE)Random.Range(0, 4);
                board[y,x].SetCell(cellParent,x,y);
            }
        }

        // ShowMap();
    }

    private void ShowMap()
    {
        for(int y = 0; y < mapSize; y++)
        {
            for(int x = 0 ; x< mapSize; x++)
            {
                board[y,x].SetCell(cellParent,x,y);
            }
        }
    }
}
