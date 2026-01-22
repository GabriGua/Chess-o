using TMPro;
using UnityEngine;

public class DebugSettings : MonoBehaviour
{
    [SerializeField] GameObject debugMode;

    [SerializeField] TextMeshProUGUI selectedPiece;
    [SerializeField] TextMeshProUGUI pieceData;
    [SerializeField] TextMeshProUGUI kingStatus;

    [SerializeField]GameManager gameManager;

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

    public void UpdateDebug()
    {
        foreach (var piece in gamePieces)
        {
            Piece pieceData = piece.GetComponent<Piece>();
            if (pieceData != null)
            {

                if (pieceData.selected == true)
                {
                    selectedPiece.text = "Current Selected Piece: " + piece.name;

                }
            }
            else
            {
                Debug.Log("nukkl");

            }
        }
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
