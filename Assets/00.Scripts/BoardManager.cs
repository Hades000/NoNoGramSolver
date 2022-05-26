using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager ins;

    public Transform cellParent;

    public Cell cellPrefabs;
    public Cell[,] board;

    public int boardSize;

    private void Awake()
    {
        if (ins == null)
            ins = this;
    }

    void Start()
    {
        InitBoard();
    }

    private void InitBoard()
    {
        board = new Cell[boardSize, boardSize];

        int width = 600 / boardSize;
        int height = 600 / boardSize;

        for (int y = 0; y < boardSize; y++)
        {
            for (int x = 0; x < boardSize; x++)
            {
                board[y, x] = Instantiate(cellPrefabs);
                board[y, x].SetCell(cellParent, width, height, x, y);
            }
        }
    }

    public void ShowMap()
    {
        for (int y = 0; y < boardSize; y++)
        {
            for (int x = 0; x < boardSize; x++)
            {
                board[y, x].RenderCell();
            }
        }
    }

    public void ChangeBoardData(int idx, CELL_TYPE cellType, CHECK_TYPE checkType)
    {
        if (checkType == CHECK_TYPE.ROW)
        {
            for (int row = 0; row < boardSize; row++)
            {
                board[idx, idx].type = cellType;
            }
        }
        else
        {
            for (int col = 0; col < boardSize; col++)
            {
                board[col, idx].type = cellType;
            }
        }
    }
}
