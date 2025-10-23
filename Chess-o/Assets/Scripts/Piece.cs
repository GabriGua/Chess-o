using UnityEngine;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour, IPointerClickHandler
{
    public ChessPieces data;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = data.Sprite;
    }

    public void MovePieceToGrid()
    {

    }

    void ShowPossiblePos()
    {
        switch (data.type)
        {
            case PieceType.King:



                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }
}
