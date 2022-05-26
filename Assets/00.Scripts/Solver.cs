using System.Collections.Generic;
using UnityEngine;

public enum CHECK_TYPE {ROW, COL}

public class Solver : MonoBehaviour
{
    public string[] inputRowHint;
    public string[] inputColHint;
    void Start()
    {
    }

    //  ¹«Á¶°Ç Ä¥ÇØÁö´Â Ä­
    private void FillAlwaysCell(string hint, CHECK_TYPE type)
    {
        if (hint.Length == 1)
        {
            int intHint = int.Parse(hint);

            if(intHint == 0)
            {

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