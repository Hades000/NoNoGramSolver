using UnityEngine;
using UnityEngine.UI;

public class BoardUI : MonoBehaviour
{
    public Text rowHintPrefabs;
    public Text colHintPrefabs;

    public Transform rowParent;
    public Transform colParent;

    public Solver solver;

    private void Start()
    {
        GenerateHintText();
    }

    private void GenerateHintText()
    {
        int boardSize = BoardManager.ins.boardSize;
        int textSize = 600 / boardSize;

        for(int i = 0; i < boardSize; i++)
        {
            Text newRowHint = Instantiate(rowHintPrefabs);
            newRowHint.transform.SetParent(rowParent);
            newRowHint.transform.localPosition = new Vector3(-445,-(i*textSize),0);
            newRowHint.rectTransform.sizeDelta = new Vector3(425,textSize,0);
        }

        for(int i = 0 ; i < boardSize; i++)
        {
            Text newColHint = Instantiate(colHintPrefabs);
            newColHint.transform.SetParent(colParent);
            newColHint.transform.localPosition = new Vector3( i * textSize, 590,0);
            newColHint.rectTransform.sizeDelta = new Vector3(textSize,550,0);
        }
    }
    private void SetHintText()
    {
    }
}
