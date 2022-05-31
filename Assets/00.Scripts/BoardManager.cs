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
        boardSize = int.Parse(DataManager.ins.loadData[0]);
        board = new Cell[boardSize, boardSize];
        GenerateBoard();
    }

    private void GenerateBoard()
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

    public int[] GetBoardCell(int fixedIdx, CHECK_TYPE type)
    {
        int[] tmp = new int[boardSize];
        string tmpResult = "";

        if(type == CHECK_TYPE.ROW)
        {
            for(int i = 0 ; i < boardSize; i++)
            {
                tmp[i] = BoardManager.ins.board[fixedIdx,i].state;
                tmpResult += (int)tmp[i] + " ";
            }
            return tmp;
        }
        else
        {
            for(int i = 0 ; i < boardSize; i++)
            {
                tmp[i] = BoardManager.ins.board[i,fixedIdx].state;
                tmpResult += (int)tmp[i] + " ";
            }
            return tmp;
        }
    }

    public void ChangeBoardData(int fixedIdx, int curIdx, int cellType, CHECK_TYPE checkType)
    {
        if (checkType == CHECK_TYPE.ROW)
        {
            board[fixedIdx, curIdx].state = cellType;
        }
        else
        {
            board[curIdx, fixedIdx].state = cellType;
        }
    }
}
