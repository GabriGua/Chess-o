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

    public void UpdatePossibleMoves(ChessPieces data, Vector2Int coordinates, bool selected)
    {
        int x = coordinates.x;
        int y = coordinates.y;
        int c = -1;
        int m = 1;
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


                    for (int i = 0; i < 3; i++)
                    {

                        for (int j = 0; j < 3; j++)
                        {

                            if (c == 0 && m == 0)
                            {

                            }
                            else
                            {
                                if (arrayCell[x + c,y + m].GetComponent<Cell>().occupiedCell == false)
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
                                
                            }



                            c++;
                        }
                        c = -1;
                        m--;
                    }


                    break;

                case PieceType.Queen:
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
