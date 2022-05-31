using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager ins;

    public string loadFileName;

    public string[] loadData;

    private void Awake()
    {
        if (ins == null)
            ins = this;
    }

    private void Start()
    {
        loadData = FileLoad(loadFileName).Split('\n');
    }

    public string FileLoad(string fileName)
    {
        string path = "C:/Users/user/Desktop/NoNoGram/hint/" + loadFileName + ".txt";
        FileInfo fileInfo = new FileInfo(path);
        var value = "";
        if (fileInfo.Exists)
        {
            StreamReader reader = new StreamReader(path);
            value = reader.ReadToEnd();
            reader.Close();
        }

        else
            value = "파일이 없습니다.";

        return value;
    }
}
