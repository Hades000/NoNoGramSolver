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
                }
                else
                {
                    string[] rowhintArr = inputRowHint[i].Split(',');

                    if(rowhintArr.Length == 2)
                    {
                        TwoHintSolve(i,rowhintArr, CHECK_TYPE.ROW);
                    }
                }

                BoardManager.ins.CheckBoardState(i, CHECK_TYPE.ROW);
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

        Debug.Log("Count : " + count);
        Debug.Log(ShowTestArray("SUM ARRAY",sum));

        for(int i = 0; i < BoardManager.ins.boardSize; i++)
        {
            CELL_TYPE changeType = sum[i] == count ? CELL_TYPE.O : CELL_TYPE.EMPTY;
            BoardManager.ins.ChangeBoardData(idx,i,changeType,type);
        }
    }

    private void MultiHintSolve(int idx, string hintStr, CHECK_TYPE type)
    {
        int len = BoardManager.ins.boardSize;
        
        string[] spltHint = hintStr.Split(',');
        int[] intHintArr = new int[spltHint.Length];
        int[] tmpArr = new int[len];
        int[] sum = new int[len];
        int count = 0;

        for(int i = 0 ; i < spltHint.Length;i++)
        {
            intHintArr[i] = int.Parse(spltHint[i]);
        }

        for(int n =0; n < Mathf.Pow(2,len); n++)
        {
            List<int> dummy = new List<int>();
            List<int> answer = new List<int>();
            
            int tmpSum = 0;
            int tmpArrSum = 0;

            dummy.Add(0);
            for(int i = 0 ; i<tmpArr.Length; i++)
            {
                dummy.Add(tmpArr[i]);
                tmpArrSum += tmpArr[i];
            }
            dummy.Add(0);

            if(tmpArrSum == 0)
            {
                answer.Add(0);
            }

            for(int i = 1 ; i<dummy.Count; i++)
            {
                tmpSum += dummy[i];

                if(dummy[i] == 1 && dummy[i+1] == 0)
                {
                    answer.Add(tmpSum);
                    tmpSum = 0;
                }
            }

            if(answer.ToArray() == intHintArr)
            {
                for(int i = 0 ; i <len; i++)
                {
                    sum[i] += tmpArr[i];
                }

                count++;
            }

            dummy[0] += 1;

            for(int i = 0 ; i < len-1; i++)
            {
                if(tmpArr[i] == 2)
                {
                    tmpArr[i] =0;
                    tmpArr[i+1] +=1;
                }
            }
        }

        Debug.Log(ShowTestArray("Multi Arr", sum));
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

    private string ShowTestArray(string title, int[] arr)
    {
        string tmp = "";

        for(int i = 0; i< arr.Length;i++)
        {
            tmp += arr[i].ToString() +" ";
        }

        return title + " : " + tmp;
    }

    private string ShowTestArray(string title, List<int> arr)
    {
        string tmp = "";

        for(int i = 0; i< arr.Count;i++)
        {
            tmp += arr[i].ToString() +" ";
        }

        return title + " : " + tmp;
    }
}