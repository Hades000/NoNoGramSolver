using UnityEngine;
using UnityEngine.UI;

public enum CELL_TYPE {EMPTY, O,X, DK}

public class Cell : MonoBehaviour
{
    public CELL_TYPE type;
    public Image img;
}
