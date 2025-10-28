using UnityEngine;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public ChessPieces data;
    [SerializeField]CellGeneration cellGeneration;
    GameManager gameManager;


    private SpriteRenderer spriteRenderer;
    public Vector2Int coordinates;
    public Cell cell;
    public bool selected = false;
    bool canMove = false;
    bool captured = false;
    void Awake()
    {
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

            
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "cell")
            {
                
                cell = collision.GetComponent<Cell>();
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
                
                cell = collision.GetComponent<Cell>();
                
                cell.DeoccupyCell();
            }

        }
    }

    public void ResetCellOccupation(Cell cell)
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
        }
        else
        {
            canMove = false;
        }
    }
}
