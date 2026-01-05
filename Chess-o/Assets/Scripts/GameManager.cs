using System.Net.NetworkInformation;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] gamePieces;

    [SerializeField] GameObject killPosGreen;
    [SerializeField] GameObject killPosPink;

    bool isGreenTurn = true;

    private void Start()
    {
        foreach (var gamePiece in gamePieces)
        {
            var pieceScript = gamePiece.GetComponent<Piece>();
            pieceScript.TurnSystem(isGreenTurn, gameObject.GetComponent<GameManager>());
            if (pieceScript.data.isGreen == true)
            {
                pieceScript.GetKillTransform(killPosGreen.transform);

            }
            else
            {
                pieceScript.GetKillTransform(killPosPink.transform);
            }
            
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
            var pieceScript = gamePiece.GetComponent<Piece>();
            pieceScript.TurnSystem(isGreenTurn, gameObject.GetComponent<GameManager>());
        }
    }

    public void ResetPieceCollider()
    {
        foreach(var gamePiece in gamePieces)
        {
            var pieceScript = gamePiece.GetComponent<Piece>();
            if (pieceScript.captured == false)
            {
                gamePiece.GetComponent<BoxCollider2D>().enabled = true;

            }
            
        }

        foreach (var gamePiece in gamePieces)
        {
            var pieceScript = gamePiece.GetComponent<Piece>();
            if (pieceScript.captured == false)
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

    GameObject GetKing(bool isGreen)
    {
        foreach (var gamePiece in gamePieces)
        {
            var pieceScript = gamePiece.GetComponent<Piece>();

            if(!pieceScript.captured && pieceScript.data.type == PieceType.King && pieceScript.data.isGreen == isGreen)
            {
                return gamePiece;
            }
        }
        return null;
    }

    bool isKingInCheck(bool isGreen, GameObject[,] arraycell)
    {
        AllPossibleMoves(arraycell);

        GameObject king = GetKing(isGreen);
        if (king == null) return false;

        Vector2Int kingPos = king.GetComponent<Piece>().coordinates;
        Cell kingCell = arraycell[kingPos.x, kingPos.y].GetComponent<Cell>();

        return kingCell.isKingCheck(isGreen);
    }

    //simulation
    public bool IsMoveLegal(GameObject pieceObj, Vector2Int targetPos, GameObject[,] arrayCell)
    {
        Piece pieceScript = pieceObj.GetComponent<Piece>();

        Vector2Int originalPos = pieceScript.coordinates;

        PositionCell fromCell = arrayCell[originalPos.x, originalPos.y].GetComponentInChildren<PositionCell>();
        PositionCell toCell = arrayCell[targetPos.x, targetPos.y].GetComponentInChildren<PositionCell>();

        Debug.Log(targetPos.x + ", " + targetPos.y);
        Debug.Log(toCell);
        Debug.Log(fromCell);

        GameObject capturedPiece = null;

        if (toCell.occupiedCell)
        {
            capturedPiece = toCell.pieceInCell;
        }

        fromCell.DeoccupyCell();


        toCell.OccupyCell(pieceObj);

        pieceScript.coordinates = targetPos;

        if (capturedPiece != null)
        {
            capturedPiece.GetComponent<Piece>().captured = true;

        }

        bool kingInCheck = isKingInCheck(pieceScript.data.isGreen, arrayCell);

        pieceScript.coordinates = originalPos;
        fromCell.OccupyCell(pieceObj);

        toCell.DeoccupyCell();
        if (capturedPiece != null)
        {
            capturedPiece.GetComponent<Piece>().captured = false;
            toCell.OccupyCell(capturedPiece);
        }

        return !kingInCheck;
    }

    public void AllPossibleMoves(GameObject[,] arrayCell)
    {
        int width = 8;
        int height = 8;

        foreach(var cell  in arrayCell)
        {
            var cellScript = cell.GetComponent<Cell>();
            cellScript.ResetKingPass();
        }    

        foreach (var gamePiece in gamePieces)
        {

            ChessPieces data = gamePiece.GetComponent<Piece>().data;
            Vector2Int coordinates =  gamePiece.GetComponent<Piece>().coordinates;


            int x = coordinates.x;
            int y = coordinates.y;

            

            switch (data.type)
            {


                case PieceType.King:

                    Vector2Int[] directionsKi = {
                        new(-1, -1),
                        new(-1, 0),
                        new(-1, 1),
                        new(0, -1),
                        new(0, 1),
                        new(1, -1),
                        new(1, 0),
                        new(1, 1)
                    };

                    foreach (Vector2Int dir in directionsKi)
                    {

                        int newX = x + dir.x;
                        int newY = y + dir.y;


                        if (newX < 0 || newX >= width || newY < 0 || newY >= height)
                            continue;

                        var cell = arrayCell[newX, newY].GetComponent<Cell>();

                        if (!cell.occupiedCell)
                        {
                            cell.CantKingPass(data.isGreen);
                        }
                        else
                        {
                            if (cell.pieceInCell.GetComponent<Piece>().data.isGreen != data.isGreen)
                            {

                                cell.CantKingPass(data.isGreen);
                            }


                        }


                    }
                    break;



                case PieceType.Queen:
                    Vector2Int[] directionsQ = {
                        new(1, 1),
                        new(-1, -1),
                        new(1, -1),
                        new(-1, 1),
                        new(0, 1),
                        new(0, -1),
                        new(1, 0),
                        new(-1, 0)
                    };

                    foreach (Vector2Int dir in directionsQ)
                    {
                        for (int step = 1; step < width; step++)
                        {
                            int newX = x + dir.x * step;
                            int newY = y + dir.y * step;


                            if (newX < 0 || newX >= width || newY < 0 || newY >= height)
                                break;

                            var cell = arrayCell[newX, newY].GetComponent<Cell>();

                            if (!cell.occupiedCell)
                            {
                                cell.CantKingPass(data.isGreen);
                            }
                            else
                            {
                                if (cell.pieceInCell.GetComponent<Piece>().data.isGreen != data.isGreen)
                                {
                                    cell.CantKingPass(data.isGreen);
                                    break;
                                }
                                else
                                {
                                    break;

                                }

                            }
                        }
                    }
                    break;



                case PieceType.Pawn:

                    
                    
                    if (data.isGreen)
                    {
                       


                        bool canAttackLeft = (x - 1 >= 0 && y + 1 < height);
                        bool canAttackRight = (x + 1 < width && y + 1 < height);


                        if (canAttackLeft)
                        {
                            
                                arrayCell[x - 1, y + 1].GetComponent<Cell>().CantKingPass(data.isGreen);
                            
                        }

                        if (canAttackRight)
                        {
                            
                                arrayCell[x + 1, y + 1].GetComponent<Cell>().CantKingPass(data.isGreen);
                            
                        }
                    }
                    else
                    {
                        

                        bool canAttackLeft = (x - 1 >= 0 && y - 1 >= 0);
                        bool canAttackRight = (x + 1 < width && y - 1 >= 0);

                        if (canAttackLeft)
                        {
                            
                                arrayCell[x - 1, y - 1].GetComponent<Cell>().CantKingPass(data.isGreen);
                            
                        }

                        if (canAttackRight)
                        {
                            
                                arrayCell[x + 1, y - 1].GetComponent<Cell>().CantKingPass(data.isGreen);
                            
                        }
                    }

                    break;
                case PieceType.Knight:
                    Vector2Int[] directionsK = {
                        new(2, -1),
                        new(2, 1),
                        new(1, 2),
                        new(-1, 2),
                        new(-2, 1),
                        new(-2, -1),
                        new(1, -2),
                        new(-1, -2)
                    };

                    foreach (Vector2Int dir in directionsK)
                    {

                        int newX = x + dir.x;
                        int newY = y + dir.y;


                        if (newX < 0 || newX >= width || newY < 0 || newY >= height)
                            continue;

                        var cell = arrayCell[newX, newY].GetComponent<Cell>();

                        if (!cell.occupiedCell)
                        {
                            cell.CantKingPass(data.isGreen);
                        }
                        else
                        {
                            if (cell.pieceInCell.GetComponent<Piece>().data.isGreen != data.isGreen)
                            {
                                cell.CantKingPass(data.isGreen);

                            }


                        }


                    }
                    break;





                case PieceType.Bishop:

                    Vector2Int[] directionsB = {
                        new(1, 1),
                        new(-1, -1),
                        new(1, -1),
                        new(-1, 1)
                    };

                    foreach (Vector2Int dir in directionsB)
                    {
                        for (int step = 1; step < width; step++)
                        {
                            int newX = x + dir.x * step;
                            int newY = y + dir.y * step;


                            if (newX < 0 || newX >= width || newY < 0 || newY >= height)
                                break;

                            var cell = arrayCell[newX, newY].GetComponent<Cell>();

                            if (!cell.occupiedCell)
                            {
                                cell.CantKingPass(data.isGreen);
                            }
                            else
                            {
                                if (cell.pieceInCell.GetComponent<Piece>().data.isGreen != data.isGreen)
                                {
                                    cell.CantKingPass(data.isGreen);
                                    break;
                                }
                                else
                                {
                                    break;

                                }

                            }
                        }
                    }
                    break;

                case PieceType.Rook:




                    Vector2Int[] directions = {
                        new(0, 1),
                        new(0, -1),
                        new(1, 0),
                        new(-1, 0)
                    };

                    foreach (Vector2Int dir in directions)
                    {
                        for (int step = 1; step < width; step++)
                        {
                            int newX = x + dir.x * step;
                            int newY = y + dir.y * step;


                            if (newX < 0 || newX >= width || newY < 0 || newY >= height)
                                break;

                            var cell = arrayCell[newX, newY].GetComponent<Cell>();

                            if (!cell.occupiedCell)
                            {
                                cell.CantKingPass(data.isGreen);
                            }
                            else
                            {
                                if (cell.pieceInCell.GetComponent<Piece>().data.isGreen != data.isGreen)
                                {
                                    cell.CantKingPass(data.isGreen);
                                    break;
                                }
                                else
                                {
                                    break;

                                }

                            }
                        }
                    }
                    break;
                default:

                    break;
            }
        }
       
    }

 


}
