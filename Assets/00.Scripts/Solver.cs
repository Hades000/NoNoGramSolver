using System.Collections.Generic;
using UnityEngine;

public class Solver : MonoBehaviour
{
    public string[] inputRowHint;
    public string[] inputColHint;
    void Start()
    {
        
    }

    //  ������ ĥ������ ĭ
    private void FillAlwaysCell(string hint)
    {
        List<int> hints = ChangeStringHint(hint);
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