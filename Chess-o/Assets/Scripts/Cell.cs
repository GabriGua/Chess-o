using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    public Vector2Int gridPosition;
    public bool occupiedCell;
    public int pieceOverCell;
    public GameObject pieceInCell;

    public GameObject selectedPiece;

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


        if (piece != null)
        {
            Piece pieceCS = piece.GetComponent<Piece>();
            pieceCS.ResetCellOccupation(gameObject.GetComponent<Cell>());
        }
        

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

    public void FindSelectedPiece(GameObject piece)
    {
        selectedPiece = piece;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Via");
        MovePieceToCell(selectedPiece, gameObject.transform.position);
    }
    public void MovePieceToCell(GameObject selectedPiece, Vector2 targetPosition)
    {
        
        StartCoroutine(MovePieceSmoothly(selectedPiece, targetPosition));
    }

    private IEnumerator MovePieceSmoothly(GameObject piece, Vector2 target)
    {
        Vector2 fixedTarget = new Vector2(target.x - 1, target.y - 1);

        while (Vector2.Distance(piece.transform.position, fixedTarget) > 0.01f)
        {
            piece.transform.position = Vector2.MoveTowards(
                piece.transform.position,
                fixedTarget,
                5f * Time.deltaTime 
            );

            yield return null; 
        }

        
        piece.transform.position = fixedTarget;
    }

}
