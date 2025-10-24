using UnityEngine;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]ChessPieces data;
    [SerializeField]CellGeneration cellGeneration;

    private SpriteRenderer spriteRenderer;
    public Vector2Int coordinates;
    public Cell cell;
    bool selected = false;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = data.Sprite;
    }

    public void MovePieceToGrid()
    {

    }


    public void OnPointerClick(PointerEventData eventData)
    {

        if (selected == true)
        {
            selected = false;
        }
        else
        {
            selected = true;
        }

            cellGeneration.UpdatePossibleMoves(data, coordinates, selected);
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
                if(cell.pieceInCell != gameObject)
                {
                    coordinates = cell.gridPosition;
                }
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
}
