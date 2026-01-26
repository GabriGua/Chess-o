using System;
using System.Net.NetworkInformation;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSettings : MonoBehaviour
{
    [SerializeField] GameObject debugMode;

    [SerializeField] TextMeshProUGUI selectedPiece;
    [SerializeField] TextMeshProUGUI pieceDataText;
    

    [SerializeField]GameManager gameManager;
    [SerializeField] CellGeneration cellGeneration;

    [SerializeField] GameObject[] gamePieces;
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gamePieces = gameManager.gamePieces;
        

    }

    public void ActivateDebugMode()
    {
        if (debugMode.activeSelf)
        {
            debugMode.SetActive(false);

        }
        else
        {
            debugMode.SetActive(true);
        }
            
    
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateDebug()
    {
        foreach (var piece in gamePieces)
        {
            Piece pieceData = piece.GetComponent<Piece>();
            ChessPieces data = pieceData.data;
            if (pieceData != null)
            {

                if (pieceData.selected == true)
                {
                    selectedPiece.text = "Current Selected Piece: " + piece.name;

                    pieceDataText.text = "Selected Piece Data: \r\n\tisGreen: " + data.isGreen +
                        "\r\n\tSelected:" + pieceData.selected + "\r\n\tcanMove:" + pieceData.canMove +
                        "\r\n\tCaptured:" + pieceData.captured + "\r\n\tposition:" + pieceData.coordinates + "\r\n";

                   
                }
            }
            
        }

        
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
