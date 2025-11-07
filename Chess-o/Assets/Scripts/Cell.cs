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

    GameManager gameManager;
    
    public void Init(Vector2Int pos)
    {
        gridPosition = pos;
        name = $"Cell {pos.x},{pos.y}";
    }

    public void PassCellInfo(bool occupation, int pieceCell, GameObject thisPiece)
    {
        occupiedCell = occupation;
        pieceOverCell = pieceCell;
        pieceInCell = thisPiece;
    }

    public void FindSelectedPiece(GameObject piece, CellGeneration cellGen)
    {
        selectedPiece = piece;
        cellGeneration = cellGen;

        

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(selectedPiece.GetComponent<Piece>().selected == true)
        {
            MovePieceToCell(selectedPiece, gameObject.transform.position);
            
            gameManager.SwitchTurn();
            gameManager.ResetPieceCollider();
            

        }
        
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
        Vector2 fixedTarget = new Vector2(target.x, target.y);
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

    public void SetGameManager(GameManager gameM)
    {
        gameManager = gameM;
    }

}
