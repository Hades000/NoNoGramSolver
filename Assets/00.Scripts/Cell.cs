using UnityEngine;
using UnityEngine.UI;


public class Cell : MonoBehaviour
{
    
    public int state;
    public Image img;

    public void SetCell(Transform parent, int width, int height, int x, int y)
    {
        state = 0;
        transform.SetParent(parent);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        transform.localPosition = new Vector2(x * width, -(y * height));

        RenderCell();
    }

    public void RenderCell()
    {
        if (state == 0 || state ==-1)
            img.color = Color.white;
        else if (state == 1)
            img.color = Color.black;
    }
}
