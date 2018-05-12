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

    private ChessBoardSetUp chessSetUp;
    private Options options; 

    void Start()
    {
        chessSetUp = FindObjectOfType<ChessBoardSetUp>();
        options = FindObjectOfType<Options>();

        pointValue = chessSetUp.ReturnPieceValue((int)piece);
        pointText.text = pointValue.ToString();
        arrowButtons[0].onClick.AddListener(() => ChangePoint(1));
        arrowButtons[1].onClick.AddListener(() => ChangePoint(-1));
    }

    void ChangePoint(int amount)
    {
        pointValue = (pointValue + amount) % 10;
        pointValue = pointValue >= 0 ? pointValue : 9;
        
        chessSetUp.SetPieceValue((int)piece, pointValue);
        options.UpdateRules();
        pointText.text = pointValue.ToString();
    }
}
