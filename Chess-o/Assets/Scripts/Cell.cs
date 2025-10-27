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
    public CellGeneration cellGeneration;
    [SerializeField] private AnimationCurve moveCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);



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

    public void FindSelectedPiece(GameObject piece, CellGeneration cellGen)
    {
        selectedPiece = piece;
        cellGeneration = cellGen;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        MovePieceToCell(selectedPiece, gameObject.transform.position);
    }
    public void MovePieceToCell(GameObject selectedPiece, Vector2 targetPosition)
    {
        if(cellGeneration != null)
        {
            cellGeneration.ResetBoard();
        }
        selectedPiece.GetComponent<Piece>().DeSelectPiece();
        StartCoroutine(MovePieceSmoothly(selectedPiece, targetPosition));
    }

   

    private IEnumerator MovePieceSmoothly(GameObject piece, Vector2 target)
    {
        Vector2 fixedTarget = new Vector2(target.x - 1, target.y - 1);
        Vector2 startPosition = piece.transform.position;
        float duration = 0.5f; 
        float time = 0f;

        while (time < duration)
        {
            
            float t = time / duration;

            
            float curveValue = moveCurve.Evaluate(t);

            
            piece.transform.position = Vector2.Lerp(startPosition, fixedTarget, curveValue);

           
            time += Time.deltaTime;

            yield return null;
        }

        
        piece.transform.position = fixedTarget;
    }


}
