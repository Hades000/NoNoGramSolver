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
        int len = BoardManager.ins.boardSize;
        inputRowHint = new string[len];
        inputColHint = new string[len];
        SetHintData();

        yield return new WaitUntil(() => BoardManager.ins.isSettingCom);
        

        for(int n = 0 ; n <len ; n++)
        {
            for (int i = 0; i < len; i++)
            {
                MultiHintSolve(i, inputRowHint[i], CHECK_TYPE.ROW); 
            }
            for (int i = 0; i < len; i++)
            {
                MultiHintSolve(i, inputColHint[i], CHECK_TYPE.COL);
            }
        }


        BoardManager.ins.ShowMap();
    }

    private void SetHintData()
    {
        Debug.Log("SetHingData Start");

        Debug.Log(DataManager.ins.loadData);

        int rowStart = 1;
        int colStart = BoardManager.ins.boardSize + 2;
        for(int i = 0 ; i< inputRowHint.Length; i++)
        {
            inputRowHint[i] = DataManager.ins.loadData[rowStart+i]; 

            Debug.Log(DataManager.ins.loadData[rowStart+i]);
        }
        for(int i = 0 ; i< inputColHint.Length; i++)
        {
            inputColHint[i] = DataManager.ins.loadData[colStart+i]; 
            Debug.Log(DataManager.ins.loadData[colStart+i]);
        }
    }

    private void MultiHintSolve(int idx, string hintStr, CHECK_TYPE type)
    {
        int len = BoardManager.ins.boardSize;

        int[] boardCellDatas = BoardManager.ins.GetBoardCell(idx,type);
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
                List<int> okArr = new List<int>();
                List<int> banArr = new List<int>();
                int okSwitch = 1;
                int banSwitch = 1;

                for(int i = 0; i< len; i++)
                {
                    if(boardCellDatas[i] == 1)
                    {
                        okArr.Add(i);
                    }
                    else if(boardCellDatas[i] ==-1)
                    {
                        banArr.Add(i);
                    }
                }


                for(int i =0; i<okArr.Count;i++)
                {
                    if(tmpArr[okArr[i]] != 1)
                    {
                        okSwitch = 0;
                    }
                }

                for(int i =0; i<banArr.Count;i++)
                {
                    if(tmpArr[banArr[i]] != 0)
                    {
                        banSwitch = 0;
                    }
                }

                if(okSwitch * banSwitch == 1)
                {
                    for(int i = 0 ; i <len; i++)
                    {
                        sum[i] += tmpArr[i];
                    }

                    count++;
                }
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
            int changeType = 0;

            if(sum[i] == count)
                changeType = 1;
            else if(sum[i] == 0)
                changeType =-1;

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
}