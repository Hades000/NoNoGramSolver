using UnityEngine;
using UnityEngine.UI;

public class BoardUI : MonoBehaviour
{
    public Text rowHintPrefabs;
    public Text colHintPrefabs;

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
            newRowHint.rectTransform.sizeDelta = new Vector2(425,textSize);
            newRowHint.transform.localPosition = new Vector2(-445,-(i*textSize));
        }

        for(int i = 0 ; i < boardSize; i++)
        {
            Text newColHint = Instantiate(rowHintPrefabs);
            newColHint.rectTransform.sizeDelta = new Vector2(textSize,550);
            newColHint.transform.localPosition = new Vector2( i * textSize, 590);
        }
    }
}
