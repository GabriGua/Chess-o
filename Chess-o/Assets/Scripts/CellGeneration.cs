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
                                        arrayCell[x + c, y + m].GetComponent<Cell>().FindSelectedPiece(selectedPiece);
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
                                spriteRenderer = arrayCell[x + c, y + m].GetComponent<SpriteRenderer>();
                                if (data.isGreen)
                                {
                                    spriteRenderer.sprite = green;
                                }
                                else
                                {
                                    spriteRenderer.sprite = pink;
                                }
                            }



                            c++;
                        }
                        c = -1;
                        m--;
                    }
                    break;

                case PieceType.Pawn:
                    
                    m = 1;

                    if (y == 1 )
                    {
                        for(int i = 0; i < 2; i++)
                        {
                            if (arrayCell[x, y + m].GetComponent<Cell>().occupiedCell == false)
                            {
                                spriteRenderer = arrayCell[x , y + m].GetComponent<SpriteRenderer>();
                                if (data.isGreen)
                                {
                                    spriteRenderer.sprite = green;
                                }
                                else
                                {
                                    spriteRenderer.sprite = pink;
                                }
                            }
                            m++;
                        }
                        
                    }
                    else
                    {
                        if (arrayCell[x, y + m].GetComponent<Cell>().occupiedCell == false)
                        {
                            spriteRenderer = arrayCell[x, y + m].GetComponent<SpriteRenderer>();
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

                        break;
                case PieceType.Knight:

                    break;
                case PieceType.Bishop:

                    break;
                case PieceType.Rook:

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
