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
                BoardManager.ins.CheckBoardState(i, CHECK_TYPE.ROW);
                BoardManager.ins.CheckBoardState(i, CHECK_TYPE.COL);
            }

            BoardManager.ins.ShowMap();

            yield return new WaitForSecondsRealtime(1f);
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