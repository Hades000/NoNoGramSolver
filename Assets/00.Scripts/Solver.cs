using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CHECK_TYPE { ROW, COL }

public class Solver : MonoBehaviour
{
    public string[] inputRowHint;
    public string[] inputColHint;

    void Start()
    {
        StartCoroutine(Solve());
    }

    private IEnumerator Solve()
    {
        yield return new WaitUntil(() => BoardManager.ins.isSettingCom);
        // while (true)
        // {
            for (int i = 0; i < BoardManager.ins.boardSize; i++)
            {
                if(!inputRowHint[i].Contains(","))
                {
                    OneHintSolve(i, inputRowHint[i], CHECK_TYPE.ROW);
                    // OneHintSolve(i, inputColHint[i], CHECK_TYPE.COL);
                }
                else
                {
                    string[] hintArr = inputRowHint[i].Split(',');

                    if(hintArr.Length == 2)
                    {
                        TwoHintSolve(i,hintArr, CHECK_TYPE.ROW);
                    }
                }

                BoardManager.ins.CheckBoardState(i, CHECK_TYPE.ROW);
                BoardManager.ins.CheckBoardState(i, CHECK_TYPE.COL);
            }

            BoardManager.ins.ShowMap();

            // yield return new WaitForSecondsRealtime(1f);
        // }
    }

    private void OneHintSolve(int idx, string hintStr, CHECK_TYPE type)
    {
        int[] sum = new int[BoardManager.ins.boardSize];

        int hint = int.Parse(hintStr);
        int loopCount = BoardManager.ins.boardSize - hint + 1;

        for(int i = 0 ; i<loopCount; i++)
        {
            int[] tmp = MakeTempList(BoardManager.ins.boardSize);

            for(int j = 0; j<hint; j++)
            {
                sum[i+j]++;
            }
        }

        for(int i = 0; i < BoardManager.ins.boardSize; i++)
        {
            CELL_TYPE changeType = sum[i] == loopCount ? CELL_TYPE.O : CELL_TYPE.X;
            BoardManager.ins.ChangeBoardData(idx,i,changeType,type);
        }
    }

    private void TwoHintSolve(int idx, string[] hintStr, CHECK_TYPE type)
    {

    }

    private int[] MakeTempList(int len)
    {
        int[] temp = new int[len]; 

        for(int i = 0 ; i < len; i++)
        {
            temp[i] = 0;
        }

        return temp;
    }

    private List<int> ChangeStringHint(string hint)
    {
        List<int> tmp = new List<int>();

        string[] tmpStr = hint.Split(',');

        for (int i = 0; i < tmpStr.Length; i++)
        {
            tmp.Add(int.Parse(tmpStr[i]));
        }

        return tmp;
    }
}