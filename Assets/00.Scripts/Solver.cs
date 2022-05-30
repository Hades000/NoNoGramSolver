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
        for (int i = 0; i < BoardManager.ins.boardSize; i++)
        {
            MultiHintSolve(i, inputRowHint[i], CHECK_TYPE.ROW);
            BoardManager.ins.CheckBoardState(i, CHECK_TYPE.ROW);
        }

        BoardManager.ins.ShowMap();
    }

    private void MultiHintSolve(int idx, string hintStr, CHECK_TYPE type)
    {
        int len = BoardManager.ins.boardSize;
        
        List<int> intHintArr = new List<int>();
        string[] spltHint = hintStr.Split(',');

        int[] tmpArr = new int[len];
        int[] sum = new int[len];
        int count = 0;

        for(int i = 0 ; i < spltHint.Length;i++)
        {
            intHintArr.Add(int.Parse(spltHint[i])); 
        }


        for(int n =0; n < Mathf.Pow(2,len); n++)
        {
            int[] dummy = new int[len+2];
            List<int> answer = new List<int>();
            
            int tmpSum = 0;

            dummy[0] = 0;

            for(int i = 0 ; i<tmpArr.Length; i++)
            {
                dummy[i+1] = tmpArr[i];
            }
            dummy[len+1]=0;
   
            for(int i = 1 ; i<dummy.Length; i++)
            {
                tmpSum += dummy[i];

                if(dummy[i] == 1 && dummy[i+1] == 0)
                {
                    answer.Add(tmpSum);
                    tmpSum = 0;
                }
            }

            if(CompareList(answer,intHintArr))
            {
                for(int i = 0 ; i <len; i++)
                {
                    sum[i] += tmpArr[i];
                }

                count++;
            }

            tmpArr[0] += 1;

            for(int i = 0 ; i < len-1; i++)
            {
                if(tmpArr[i] == 2)
                {
                    tmpArr[i] =0;
                    tmpArr[i+1] +=1;
                }
            }
        }

        for(int i = 0; i < BoardManager.ins.boardSize; i++)
        {
            CELL_TYPE changeType = sum[i] == count ? CELL_TYPE.O : CELL_TYPE.EMPTY;
            BoardManager.ins.ChangeBoardData(idx,i,changeType,type);
        }
    }
    private bool CompareList(List<int>a, List<int>b)
    {
        if(a.Count != b.Count)
            return false;
        else
        {
            for(int i = 0 ; i<a.Count; i++)
            {
                if(a[i] != b[i])
                    return false;
            }

            return true;
        }
    }
    private void ShowTestArray(string title, int[] arr)
    {
        string tmp = "";

        for(int i = 0; i< arr.Length;i++)
        {
            tmp += arr[i].ToString() +" ";
        }

        Debug.Log(title + " : " + tmp);
    }
    private void ShowTestArray(string title, List<int> arr)
    {
        string tmp = "";

        for(int i = 0; i< arr.Count;i++)
        {
            tmp += arr[i].ToString() +" ";
        }

        Debug.Log(title + " : " + tmp);
    }
}