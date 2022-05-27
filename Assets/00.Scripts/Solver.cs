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

    }

    private IEnumerator Solve()
    {
        while(true)
        {
            for(int i = 0; i < BoardManager.ins.boardSize; i++)
            {
                FillAlwaysCell(i,inputRowHint[i],CHECK_TYPE.ROW);
            }

            yield return new WaitForSecondsRealtime(1f);
        }
    }

    //  ������ ĥ������ ĭ
    private void FillAlwaysCell(int idx, string hint, CHECK_TYPE type)
    {
        if (hint.Length == 1)
        {
            int intHint = int.Parse(hint);

            if (intHint == BoardManager.ins.boardSize)
            {
                BoardManager.ins.ChangeBoardData(idx, CELL_TYPE.O, CHECK_TYPE.ROW);
            }
        }
        else
        {
            List<int> hints = ChangeStringHint(hint);
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