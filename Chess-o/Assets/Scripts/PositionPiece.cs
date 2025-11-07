using UnityEngine;

public class PositionPiece : MonoBehaviour
{
    ChessPieces chessPieces;
    bool canMovie;

   


    public void GetPieceData(ChessPieces Data, bool canMove)
    {
        chessPieces = Data;
        canMovie = canMove;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            

            if (collision.gameObject.tag == "piece")
            {
                if (collision.gameObject.GetComponentInParent<Piece>().data.isGreen != chessPieces.isGreen)
                {
                    if (canMovie == false)
                    {

                        collision.gameObject.GetComponentInParent<Piece>().IsCaptured();
                    }

                }

            }

        }
    }

   
    
}
