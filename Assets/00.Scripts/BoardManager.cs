using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public static BoardManager ins;

    public Transform cellParent;

    public Cell cellPrefabs;
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
        int width = 600 / mapSize;
        int height = 600 / mapSize;

        Debug.Log("Cell Width : " + width);
        Debug.Log("Cell Height : " + height);

        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                board[y, x] = Instantiate(cellPrefabs);
                board[y, x].SetCell(cellParent, width, height, x, y);
            }
        }
    }

    private void ShowMap()
    {
        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                board[y, x].RenderCell();
            }
        }
    }
}
