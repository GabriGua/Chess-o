using UnityEngine;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public ChessPieces data;
    [SerializeField]CellGeneration cellGeneration;
    GameManager gameManager;


    private SpriteRenderer spriteRenderer;
    public Vector2Int coordinates;
    public PositionCell cell;
    public bool selected = false;
    [SerializeField]bool canMove = false;
    public bool captured = false;


    PositionPiece positionPiece;

    void Awake()
    {
        positionPiece = GetComponentInChildren<PositionPiece>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = data.Sprite;
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        if(canMove)
        {
            if (selected == true)
            {
                selected = false;
            }
            else
            {
                selected = true;
            }

            cellGeneration.UpdatePossibleMoves(data, coordinates, selected, gameObject);
            Debug.Log("Click");
            
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "cell")
            {
                
                cell = collision.GetComponentInChildren<PositionCell>();
                coordinates = cell.gridPosition;
                cell.OccupyCell(gameObject);
                
            }

            

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "cell")
            {
                
                cell = collision.GetComponentInChildren<PositionCell>();

                cell.DeoccupyCell();
            }

        }
    }

    public void ResetCellOccupation(PositionCell cell)
    {
        
        coordinates = cell.gridPosition;
    }

    public void DeSelectPiece()
    {
        selected = false;
    }

    public void TurnSystem(bool greenTurn, GameManager gameM)
    {

        if (gameManager == null)
        {
            gameManager = gameM;
        }

        if (greenTurn == data.isGreen)
        {
            
            canMove = true;
            positionPiece.GetPieceData(data,canMove);
            
        }
        else
        {
            canMove = false;
            positionPiece.GetPieceData(data, canMove);
        }
    }

    public void IsCaptured()
    {
        captured = true;
        gameObject.SetActive(false);
    }
}
