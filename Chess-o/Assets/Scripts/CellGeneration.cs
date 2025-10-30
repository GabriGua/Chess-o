using JetBrains.Annotations;
using UnityEngine;

public class CellGeneration : MonoBehaviour
{
    public GameObject cellPrefab;

    int width = 8;
    int height = 8;
    float cellSize = 2;
    GameObject[,] arrayCell;

    [SerializeField] Sprite pink;
    [SerializeField] Sprite green;

    [SerializeField] GameManager gameManager;

    GameObject selectedPiece;

    void Start()
    {
        arrayCell = new GameObject[width, height];
        GenerateBoard();
    }

    void GenerateBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                GameObject newCell = Instantiate(cellPrefab, transform);
                newCell.transform.position = new Vector3(i * cellSize, j * cellSize, 0);
                newCell.GetComponent<Cell>().Init(new Vector2Int(i, j));
                newCell.GetComponent<Cell>().SetGameManager(gameManager);
                if(j <= 1 || j >= width -1)
                {
                    newCell.GetComponent<Cell>().OccupyCell(null);
                    newCell.GetComponent<Cell>().pieceOverCell--;
                    
                }
                arrayCell[i,j] = newCell;

            }
        }

    }

    public void ResetBoard()
    {
        SpriteRenderer spriteRenderer;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                spriteRenderer = arrayCell[i, j].GetComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = 0;
                spriteRenderer.sprite = null;

            }
        }
    }
    public void UpdatePossibleMoves(ChessPieces data, Vector2Int coordinates, bool selected, GameObject currentPiece)
    {
        selectedPiece = currentPiece;
        Debug.Log(selectedPiece);
        int x = coordinates.x;
        int y = coordinates.y;
        
        int m;

        SpriteRenderer spriteRenderer;
        for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    spriteRenderer = arrayCell[i, j].GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = null;

                }
            }
        if(selected)
        { 
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
                            spriteRenderer = arrayCell[newX, newY].GetComponent<SpriteRenderer>();
                            cell.FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());
                            spriteRenderer.sprite = data.isGreen ? green : pink;
                            spriteRenderer.sortingOrder = 10;
                        }
                        else
                        {
                            if (cell.pieceInCell.GetComponent<Piece>().data.isGreen != data.isGreen)
                            {
                                spriteRenderer = arrayCell[newX, newY].GetComponent<SpriteRenderer>();
                                cell.FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());
                                spriteRenderer.sprite = data.isGreen ? green : pink;
                                spriteRenderer.sortingOrder = 10;

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
                                spriteRenderer = arrayCell[newX, newY].GetComponent<SpriteRenderer>();
                                cell.FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());
                                spriteRenderer.sprite = data.isGreen ? green : pink;
                                spriteRenderer.sortingOrder = 10;
                            }
                            else
                            {
                                if (cell.pieceInCell.GetComponent<Piece>().data.isGreen != data.isGreen)
                                {
                                    spriteRenderer = arrayCell[newX, newY].GetComponent<SpriteRenderer>();
                                    cell.FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());
                                    spriteRenderer.sprite = data.isGreen ? green : pink;
                                    spriteRenderer.sortingOrder = 10;
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
                        if (y + m < height && !arrayCell[x, y + m].GetComponent<Cell>().occupiedCell)
                        {
                            spriteRenderer = arrayCell[x, y + m].GetComponent<SpriteRenderer>();
                            arrayCell[x, y + m].GetComponent<Cell>().FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());
                            spriteRenderer.sprite = green;
                            spriteRenderer.sortingOrder = 10;


                            if (y == 1 && !arrayCell[x, y + 2].GetComponent<Cell>().occupiedCell)
                            {
                                spriteRenderer = arrayCell[x, y + 2].GetComponent<SpriteRenderer>();
                                arrayCell[x, y + 2].GetComponent<Cell>().FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());
                                spriteRenderer.sprite = green;
                                spriteRenderer.sortingOrder = 10;
                            }
                        }

                        
                        bool canAttackLeft = (x - 1 >= 0 && y + 1 < height);
                        bool canAttackRight = (x + 1 < width && y + 1 < height);

                        if (canAttackLeft && arrayCell[x - 1, y + 1].GetComponent<Cell>().occupiedCell)
                        {
                            var targetPiece = arrayCell[x - 1, y + 1].GetComponent<Cell>().pieceInCell.GetComponent<Piece>();
                            if (targetPiece.data.isGreen != data.isGreen)
                            {
                                spriteRenderer = arrayCell[x - 1, y + 1].GetComponent<SpriteRenderer>();
                                arrayCell[x - 1, y + 1].GetComponent<Cell>().FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());
                                spriteRenderer.sprite = green;
                                spriteRenderer.sortingOrder = 10;
                            }
                        }

                        if (canAttackRight && arrayCell[x + 1, y + 1].GetComponent<Cell>().occupiedCell)
                        {
                            var targetPiece = arrayCell[x + 1, y + 1].GetComponent<Cell>().pieceInCell.GetComponent<Piece>();
                            if (targetPiece.data.isGreen != data.isGreen)
                            {
                                spriteRenderer = arrayCell[x + 1, y + 1].GetComponent<SpriteRenderer>();
                                arrayCell[x + 1, y + 1].GetComponent<Cell>().FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());
                                spriteRenderer.sprite = green;
                                spriteRenderer.sortingOrder = 10;
                            }
                        }
                    }
                    else
                    {
                        if(y - m >= 0 && !arrayCell[x, y - m].GetComponent<Cell>().occupiedCell)
                        {
                            spriteRenderer = arrayCell[x, y - m].GetComponent<SpriteRenderer>();
                            arrayCell[x, y - m].GetComponent<Cell>().FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());
                            spriteRenderer.sprite = pink;
                            spriteRenderer.sortingOrder = 10;

                            if (y == 6 && !arrayCell[x, y - 2].GetComponent<Cell>().occupiedCell)
                            {
                                spriteRenderer = arrayCell[x, y - 2].GetComponent<SpriteRenderer>();
                                arrayCell[x, y - 2].GetComponent<Cell>().FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());
                                spriteRenderer.sprite = pink;
                                spriteRenderer.sortingOrder = 10;
                            }
                        }

                        bool canAttackLeft = (x - 1 >= 0 && y - 1 >= 0);
                        bool canAttackRight = (x + 1 < width && y - 1 >= 0);

                        if (canAttackLeft && arrayCell[x - 1, y - 1].GetComponent<Cell>().occupiedCell)
                        {
                            var targetPiece = arrayCell[x - 1, y - 1].GetComponent<Cell>().pieceInCell.GetComponent<Piece>(); 
                            
                            if (targetPiece.data.isGreen != data.isGreen)
                            {
                                spriteRenderer = arrayCell[x - 1, y - 1].GetComponent<SpriteRenderer>();
                                arrayCell[x - 1, y - 1].GetComponent<Cell>().FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());
                                spriteRenderer.sprite = pink;
                                spriteRenderer.sortingOrder = 10;
                            }
                        }

                        if (canAttackRight && arrayCell[x + 1, y - 1].GetComponent<Cell>().occupiedCell)
                        {
                            var targetPiece = arrayCell[x + 1, y - 1].GetComponent<Cell>().pieceInCell.GetComponent<Piece>();
                            
                            if (targetPiece.data.isGreen != data.isGreen)
                            {
                                spriteRenderer = arrayCell[x + 1, y - 1].GetComponent<SpriteRenderer>();
                                arrayCell[x + 1, y - 1].GetComponent<Cell>().FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());
                                spriteRenderer.sprite = pink;
                                spriteRenderer.sortingOrder = 10;
                            }
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
                                spriteRenderer = arrayCell[newX, newY].GetComponent<SpriteRenderer>();
                                cell.FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());
                                spriteRenderer.sprite = data.isGreen ? green : pink;
                                spriteRenderer.sortingOrder = 10;
                            }
                            else
                            {
                                if(cell.pieceInCell.GetComponent<Piece>().data.isGreen != data.isGreen)
                                {
                                    spriteRenderer = arrayCell[newX, newY].GetComponent<SpriteRenderer>();
                                    cell.FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());
                                    spriteRenderer.sprite = data.isGreen ? green : pink;
                                    spriteRenderer.sortingOrder = 10;

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
                                spriteRenderer = arrayCell[newX, newY].GetComponent<SpriteRenderer>();
                                cell.FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());
                                spriteRenderer.sprite = data.isGreen ? green : pink;
                                spriteRenderer.sortingOrder = 10;
                            }
                            else
                            {
                                if (cell.pieceInCell.GetComponent<Piece>().data.isGreen != data.isGreen)
                                {
                                    spriteRenderer = arrayCell[newX, newY].GetComponent<SpriteRenderer>();
                                    cell.FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());
                                    spriteRenderer.sprite = data.isGreen ? green : pink;
                                    spriteRenderer.sortingOrder = 10;
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
                                spriteRenderer = arrayCell[newX, newY].GetComponent<SpriteRenderer>();
                                cell.FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());
                                spriteRenderer.sprite = data.isGreen ? green : pink;
                                spriteRenderer.sortingOrder = 10;
                            }
                            else
                            {
                                if(cell.pieceInCell.GetComponent<Piece>().data.isGreen != data.isGreen)
                                {
                                spriteRenderer = arrayCell[newX, newY].GetComponent<SpriteRenderer>();
                                cell.FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());
                                spriteRenderer.sprite = data.isGreen ? green : pink;
                                spriteRenderer.sortingOrder = 10;
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
        else
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    spriteRenderer = arrayCell[i, j].GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = null;

                }
            }
        }
        

    }
}
