using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] gamePieces;
    
    bool isGreenTurn = true;

    private void Start()
    {
        foreach (var gamePiece in gamePieces)
        {
            gamePiece.GetComponent<Piece>().TurnSystem(isGreenTurn, gameObject.GetComponent<GameManager>());
        }
    }

   

    public void SwitchTurn()
    {
        if (isGreenTurn)
        {
            isGreenTurn = false;
        }
        else
        {
            isGreenTurn = true;
        }

        foreach (var gamePiece in gamePieces)
        {
            gamePiece.GetComponent<Piece>().TurnSystem(isGreenTurn, gameObject.GetComponent<GameManager>());
        }
    }

    public void ResetPieceCollider()
    {
        foreach(var gamePiece in gamePieces)
        {
            if(gamePiece.GetComponent<Piece>().captured == false)
            {
                gamePiece.GetComponent<BoxCollider2D>().enabled = true;

            }
            
        }

        foreach (var gamePiece in gamePieces)
        {
            if (gamePiece.GetComponent<Piece>().captured == false)
            {
                if(gamePiece.GetComponent<SpriteRenderer>().flipY != true)
                {
                    gamePiece.GetComponent<SpriteRenderer>().flipY = true;

                }else
                {
                    gamePiece.GetComponent<SpriteRenderer>().flipY = false;
                }
                

            }

        }
    }



}
