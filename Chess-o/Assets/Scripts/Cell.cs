using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int gridPosition;
    

    public void Init(Vector2Int pos)
    {
        gridPosition = pos;
        name = $"Cell {pos.x},{pos.y}";
    }

    
}
