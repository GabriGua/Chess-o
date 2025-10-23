using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "ChessPiece", menuName = "ScriptableObjects/ChessPiece" )]
public class ChessPieces : ScriptableObject
{   
   
    public Sprite Sprite;
    public int value;
    public bool isGreen;
    public PieceType type;

}

public enum PieceType { King, Queen, Rook, Bishop, Knight, Pawn }

