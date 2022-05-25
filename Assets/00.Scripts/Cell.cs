using UnityEngine;
using UnityEngine.UI;

public enum CELL_TYPE {EMPTY, O,X, DK}

public class Cell : MonoBehaviour
{
    public CELL_TYPE type;
    public Image img;

    public void SetCell(Transform parent, int x, int y)
    {
        transform.SetParent(parent);
        transform.localPosition = new Vector2(x * 70, -(y * 70));

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
