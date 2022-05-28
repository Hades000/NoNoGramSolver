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
                    string[] rowhintArr = inputRowHint[i].Split(',');
                    string[] colhintArr = inputColHint[i].Split(',');

                    if(rowhintArr.Length == 2)
                    {
                        TwoHintSolve(i,rowhintArr, CHECK_TYPE.ROW);
                    }

                    if(colhintArr.Length == 2)
                    {
                        TwoHintSolve(i,rowhintArr, CHECK_TYPE.COL);
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
            for(int j = 0; j<hint; j++)
            {
                sum[i+j]++;
            }
        }

        for(int i = 0; i < BoardManager.ins.boardSize; i++)
        {
            CELL_TYPE changeType = sum[i] == loopCount ? CELL_TYPE.O : CELL_TYPE.EMPTY;
            BoardManager.ins.ChangeBoardData(idx,i,changeType,type);
        }
    }

    private void TwoHintSolve(int idx, string[] hintStr, CHECK_TYPE type)
    {
        int firstHint = int.Parse(hintStr[0]);
        int secHint = int.Parse(hintStr[1]);
        int loopCount = BoardManager.ins.boardSize - secHint - firstHint;
        int count = 0;
        int[] sum = new int[BoardManager.ins.boardSize];

        for(int i = 0; i < loopCount; i++)
        {
            for(int j = i + firstHint+1;j < BoardManager.ins.boardSize-secHint+1; j++)
            {
                int[] temp = new int[BoardManager.ins.boardSize];
                for(int k = i ; k< i+firstHint; k++)
                {
                    temp[k] = 1;
                }

                for (int k = j; k < j+secHint; k++)
                {
                    temp[k] = 1;
                }

                for(int k = 0 ; k< BoardManager.ins.boardSize; k++)
                {
                    sum[k] += temp[k];
                }

                count++;
            }
        }

        for(int i = 0; i < BoardManager.ins.boardSize; i++)
        {
            CELL_TYPE changeType = sum[i] == count ? CELL_TYPE.O : CELL_TYPE.EMPTY;
            BoardManager.ins.ChangeBoardData(idx,i,changeType,type);
        }
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