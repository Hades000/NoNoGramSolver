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
        while (true)
        {
            for (int i = 0; i < BoardManager.ins.boardSize; i++)
            {
                FillAlwaysCell(i, inputRowHint, CHECK_TYPE.ROW);
                FillAlwaysCell(i, inputColHint, CHECK_TYPE.COL);
                BoardManager.ins.CheckBoardState(i,CHECK_TYPE.ROW);
                BoardManager.ins.CheckBoardState(i,CHECK_TYPE.COL);
            }

            BoardManager.ins.ShowMap();

            yield return new WaitForSecondsRealtime(1f);
        }
    }

    //  ¹«Á¶°Ç Ä¥ÇØÁö´Â Ä­
    private void FillAlwaysCell(int idx, string[] hint, CHECK_TYPE type)
    {
        if (!hint[idx].Contains(","))
        {
            int intHint = int.Parse(hint[idx]);

            if (intHint == BoardManager.ins.boardSize)
            {
                BoardManager.ins.ChangeBoardData(idx, CELL_TYPE.O, type);
                
            }
        }
        else
        {
            List<int> hints = ChangeStringHint(hint[idx]);
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