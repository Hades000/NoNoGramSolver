using UnityEngine;
using UnityEngine.UI;

public enum CELL_TYPE { EMPTY, O, X, DK }

public class Cell : MonoBehaviour
{
    
    public CELL_TYPE type;
    public Image img;

    public void SetCell(Transform parent, int width, int height, int x, int y)
    {
        type = CELL_TYPE.EMPTY;
        transform.SetParent(parent);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        transform.localPosition = new Vector2(x * width, -(y * height));

        RenderCell();
    }

    public void RenderCell()
    {
        if (type == CELL_TYPE.EMPTY)
            img.color = Color.white;
        else if (type == CELL_TYPE.O)
            img.color = Color.black;
        else if (type == CELL_TYPE.X)
            img.color = Color.red;
        else if (type == CELL_TYPE.DK)
            img.color = Color.grey;
    }
}
