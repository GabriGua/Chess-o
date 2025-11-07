using UnityEngine;

public class PositionCell : MonoBehaviour
{
    public Vector2Int gridPosition;
    public bool occupiedCell;
    public int pieceOverCell;
    public GameObject pieceInCell;

    

    GameManager gameManager;
    Cell cell;

    private void Awake()
    {
        cell = GetComponentInParent<Cell>();
    }


    public void Init(Vector2Int pos)
    {
        gridPosition = pos;
        name = $"Cell {pos.x},{pos.y}";
    }


    public void OccupyCell(GameObject piece)
    {
        occupiedCell = true;
        pieceInCell = piece;
        pieceOverCell++;


        if (piece != null)
        {
            Piece pieceCS = piece.GetComponent<Piece>();
            pieceCS.ResetCellOccupation(gameObject.GetComponent<PositionCell>());
        }
        cell.PassCellInfo(occupiedCell, pieceOverCell, pieceInCell);

    }

    public void DeoccupyCell()
    {

        if (pieceOverCell == 1)
        {
            occupiedCell = false;
            pieceInCell = null;
        }

        pieceOverCell--;
        cell.PassCellInfo(occupiedCell, pieceOverCell, pieceInCell);

    }

    public void SetGameManager(GameManager gameM)
    {
        gameManager = gameM;
    }
}
