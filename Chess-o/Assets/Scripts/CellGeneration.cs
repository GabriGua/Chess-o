using JetBrains.Annotations;
using UnityEngine;

public class CellGeneration : MonoBehaviour
{
    public GameObject cellPrefab;

    int width = 8;
    int height = 8;
    float cellSize = 2;


    void Start()
    {
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

            }
        }

    }
}
