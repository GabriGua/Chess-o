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


    

}
