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
                spriteRenderer.sprite = null;

            }
        }
    }
    public void UpdatePossibleMoves(ChessPieces data, Vector2Int coordinates, bool selected, GameObject currentPiece)
    {
        selectedPiece = currentPiece;
        int x = coordinates.x;
        int y = coordinates.y;
        int c;
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

                    Debug.Log("king");

                    //offset
                     c = -1;
                     m = 1;

                    for (int i = 0; i < 3; i++)
                    {

                        for (int j = 0; j < 3; j++)
                        {

                            if (c == 0 && m == 0)
                            {

                            }
                            else
                            {
                                //border chessboard check
                                int checkX = x + c;
                                int checkY = y + m;

                                if(checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
                                {
                                    if (arrayCell[x + c,y + m].GetComponent<Cell>().occupiedCell == false)
                                    {
                                        spriteRenderer = arrayCell[x + c, y + m].GetComponent<SpriteRenderer>();
                                        arrayCell[x + c, y + m].GetComponent<Cell>().FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());
                                        if (data.isGreen)
                                        {
                                            spriteRenderer.sprite = green;
                                        }
                                        else
                                        {
                                            spriteRenderer.sprite = pink;
                                        }
                                    }
                                }
                                
                                
                            }



                            c++;
                        }
                        c = -1;
                        m--;
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
                            }
                            else
                            {

                                break;
                            }
                        }
                    }
                    break;

                    

                case PieceType.Pawn:
                    
                    m = 1;
                    if (data.isGreen)
                    {
                        if (y == 1)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                if (arrayCell[x, y + m].GetComponent<Cell>().occupiedCell == false)
                                {
                                    spriteRenderer = arrayCell[x, y + m].GetComponent<SpriteRenderer>();
                                    arrayCell[x, y + m].GetComponent<Cell>().FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());

                                    spriteRenderer.sprite = green;

                                }
                                m++;
                            }

                        }
                        else
                        {
                            if (arrayCell[x, y + m].GetComponent<Cell>().occupiedCell == false)
                            {
                                spriteRenderer = arrayCell[x, y + m].GetComponent<SpriteRenderer>();
                                arrayCell[x, y + m].GetComponent<Cell>().FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());

                                spriteRenderer.sprite = green;

                            }
                        }
                    }
                    else
                    {
                        if (y == 6)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                if (arrayCell[x, y - m].GetComponent<Cell>().occupiedCell == false)
                                {
                                    spriteRenderer = arrayCell[x, y - m].GetComponent<SpriteRenderer>();
                                    arrayCell[x, y - m].GetComponent<Cell>().FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());

                                    spriteRenderer.sprite = pink;

                                }
                                m++;
                            }

                        }
                        else
                        {
                            if (arrayCell[x, y - m].GetComponent<Cell>().occupiedCell == false)
                            {
                                spriteRenderer = arrayCell[x, y - m].GetComponent<SpriteRenderer>();
                                arrayCell[x, y - m].GetComponent<Cell>().FindSelectedPiece(selectedPiece, gameObject.GetComponent<CellGeneration>());

                                spriteRenderer.sprite = pink;

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
                            }
                            
                        
                    }
                    break;




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
                            }
                            else
                            {

                                break;
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
                            }
                            else
                            {
                                
                                break;
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
