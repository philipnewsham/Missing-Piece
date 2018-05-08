using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePointValue : MonoBehaviour
{
    public enum PIECE
    {
        QUEEN,
        BISHOP,
        KNIGHT,
        ROOK,
        PAWN
    }

    public PIECE piece;
    private int pointValue;
    public Text pointText;
    public Button[] arrowButtons;

    void Start()
    {
        pointValue = FindObjectOfType<ChessBoardSetUp>().ReturnPieceValue((int)piece);
        pointText.text = pointValue.ToString();
        arrowButtons[0].onClick.AddListener(() => ChangePoint(1));
        arrowButtons[1].onClick.AddListener(() => ChangePoint(-1));
    }

    void ChangePoint(int amount)
    {
        pointValue = (pointValue + amount) % 10;
        pointValue = pointValue >= 0 ? pointValue : 9;
        
        FindObjectOfType<ChessBoardSetUp>().SetPieceValue((int)piece, pointValue);
        pointText.text = pointValue.ToString();
    }
}
