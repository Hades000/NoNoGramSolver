using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager ins;

    public Transform cellParent;

    public Cell cellPrefabs;
    public Cell[,] board;
    public bool[] rowCheck;
    public bool[] colCheck;

    public int boardSize;

    public bool isSettingCom = false;

    private void Awake()
    {
        if (ins == null)
            ins = this;
    }

    void Start()
    {
        board = new Cell[boardSize, boardSize];
        rowCheck = new bool[boardSize];
        colCheck = new bool[boardSize];
        InitBoard();
    }

    private void InitBoard()
    {
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

        for (int i = 0; i < boardSize; i++)
        {
            rowCheck[i] = false;
            colCheck[i] = false;
        }

        isSettingCom = true;
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

    public void CheckBoardState(int idx, CHECK_TYPE type)
    {
        if (!CanWork(idx, type))
            return;

        if (type == CHECK_TYPE.ROW)
        {
            for (int row = 0; row < boardSize; row++)
            {
                if (board[idx, row].type == CELL_TYPE.EMPTY)
                {
                    rowCheck[idx] = false;
                    Debug.Log("Empty Ä­ÀÌ ÀÖÀ½ false");
                    return;
                }
            }

            rowCheck[idx] = true;
            Debug.Log("Empty Ä­ÀÌ ¾øÀ½ true");
        }
        else
        {
            for (int col = 0; col < boardSize; col++)
            {
                if (board[col, idx].type == CELL_TYPE.EMPTY)
                {
                    colCheck[idx] = false;
                    Debug.Log("Empty Ä­ÀÌ ÀÖÀ½ false");
                    return;
                }
            }
            colCheck[idx] = true;
            Debug.Log("Empty Ä­ÀÌ ¾øÀ½ true");
        }
    }

    public void ChangeBoardData(int idx, CELL_TYPE cellType, CHECK_TYPE checkType)
    {
        if (checkType == CHECK_TYPE.ROW)
        {
            for (int row = 0; row < boardSize; row++)
            {
                board[idx, row].type = cellType;
                Debug.Log($"[{idx},{row}] Type : {checkType.ToString()}");
            }
        }
        else
        {
            for (int col = 0; col < boardSize; col++)
            {
                board[col, idx].type = cellType;
                Debug.Log($"[{col},{idx}] Type : {checkType.ToString()}");
            }
        }
    }

    public bool CanWork(int idx, CHECK_TYPE type)
    {
        return (type == CHECK_TYPE.ROW && !BoardManager.ins.rowCheck[idx] || type == CHECK_TYPE.COL && !BoardManager.ins.colCheck[idx]);
    }
}
