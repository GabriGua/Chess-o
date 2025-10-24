using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int gridPosition;
    public bool occupiedCell;
    public int pieceOverCell;
    public GameObject pieceInCell; 

    public void Init(Vector2Int pos)
    {
        gridPosition = pos;
        name = $"Cell {pos.x},{pos.y}";
    }

    private void Start()
    {
        
    }

   public void OccupyCell(GameObject piece)
    {
        occupiedCell = true;
        pieceInCell = piece;
        pieceOverCell++;
    }

    public void DeoccupyCell()
    {
        if(pieceOverCell  == 1)
        {
            occupiedCell = false;
            pieceInCell = null;
        }

        pieceOverCell--;
        
    }

}
