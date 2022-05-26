using System.Collections.Generic;
using UnityEngine;

public class Solver : MonoBehaviour
{
    public string[] inputRowHint;
    public string[] inputColHint;
    void Start()
    {
        
    }

    //  ¹«Á¶°Ç Ä¥ÇØÁö´Â Ä­
    private void FillAlwaysCell(Cell cell, string hint)
    {
    }

    private List<int> ChangeStringHint(string hint)
    {
        List<int> tmp = new List<int>();

        if(hint.Length == 0)
        {
            Debug.Log("ChangeStringHint Error : Hint Length Null");
            return null;
        }
        else if(hint.Length == 1)
        {
            tmp.Add(int.Parse(hint));
            return tmp;
        }
        else
        {
            string[] tmpStr = hint.Split(',');

            for(int i = 0; i < tmpStr.Length;i++)
            {
                tmp.Add(int.Parse(tmpStr[i]));
            }

            return tmp;
        }
    }
}