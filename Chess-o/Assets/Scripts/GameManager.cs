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
            gamePiece.GetComponent<Piece>().TurnSystem(isGreenTurn, gameObject.GetComponent<GameManager>());
            if (gamePiece.GetComponent<Piece>().data.isGreen == true)
            {
                gamePiece.GetComponent<Piece>().GetKillTransform(killPosGreen.transform);

            }
            else
            {
                gamePiece.GetComponent<Piece>().GetKillTransform(killPosPink.transform);
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


    public void AllPossibleMoves(GameObject[,] arrayCell)
    {
        int width = 8;
        int height = 8;

        foreach(var cell  in arrayCell)
        {
            cell.GetComponent<Cell>().ResetKingPass();
        }    

        foreach (var gamePiece in gamePieces)
        {

            ChessPieces data = gamePiece.GetComponent<Piece>().data;
            Vector2Int coordinates =  gamePiece.GetComponent<Piece>().coordinates;


            int x = coordinates.x;
            int y = coordinates.y;

            int m;

            switch (data.type)
            {


                case PieceType.King:

                    Vector2Int[] directionsKi = {
                        new Vector2Int(-1, -1),
                        new Vector2Int(-1, 0),
                        new Vector2Int(-1, 1),
                        new Vector2Int(0, -1),
                        new Vector2Int(0, 1),
                        new Vector2Int(1, -1),
                        new Vector2Int(1, 0),
                        new Vector2Int(1, 1)
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
                        new Vector2Int(1, 1),
                        new Vector2Int(-1, -1),
                        new Vector2Int(1, -1),
                        new Vector2Int(-1, 1),
                        new Vector2Int(0, 1),
                        new Vector2Int(0, -1),
                        new Vector2Int(1, 0),
                        new Vector2Int(-1, 0)
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

                    m = 1;
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
                        new Vector2Int(2, -1),
                        new Vector2Int(2, 1),
                        new Vector2Int(1, 2),
                        new Vector2Int(-1, 2),
                        new Vector2Int(-2, 1),
                        new Vector2Int(-2, -1),
                        new Vector2Int(1, -2),
                        new Vector2Int(-1, -2)
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
                        new Vector2Int(1, 1),
                        new Vector2Int(-1, -1),
                        new Vector2Int(1, -1),
                        new Vector2Int(-1, 1)
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
                        new Vector2Int(0, 1),
                        new Vector2Int(0, -1),
                        new Vector2Int(1, 0),
                        new Vector2Int(-1, 0)
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
