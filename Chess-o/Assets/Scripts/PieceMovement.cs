using UnityEngine;

public class PieceMovement : MonoBehaviour
{
     Piece piece;

    private void Start()
    {
        piece = gameObject.GetComponent<Piece>();
    }
    public Vector2Int coordinates;
    public Cell cell;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider != null)
        {
            if(collision.collider.gameObject.tag == "cell")
            {
                Debug.Log("cell");
                cell = collision.collider.GetComponent<Cell>();
                            coordinates = cell.gridPosition;
            }
            
        }
    }


}
