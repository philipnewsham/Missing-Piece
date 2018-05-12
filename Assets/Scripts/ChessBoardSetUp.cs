using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessBoardSetUp : MonoBehaviour
{
    public GameObject piecePrefab;

    public Transform pieceParent;
    private float gridSpace;
    private float gridOrigin;

    public bool editedScores;

    [HideInInspector]
    private int[] pieceScores = new int[5] { 9, 3, 3, 5, 1 }; //queen, bishop, knight, rook, pawn

    private int[] gameScores;
    ChessController chessController;
    public Sprite[] pieceSprites;
    private List<string> pieceNames = new List<string>(){"Queen","Bishop","Knight","Rook","Pawn"};
    
    void Start()
    {
        chessController = GetComponent<ChessController>();
        gridSpace = chessController.ReturnGridSize();
        gridOrigin = chessController.ReturnOrigin().x;
    }

    public void SetUpGame()
    { 
        if (!editedScores)
        {
            gameScores = new int[5];
            gameScores = pieceScores;
        }

        bool isWhite = true;
        CreatePiece(new Vector2(0, 0), isWhite, PieceTitle.Piece.ROOK);
        CreatePiece(new Vector2(1, 0), isWhite, PieceTitle.Piece.KNIGHT);
        CreatePiece(new Vector2(2, 0), isWhite, PieceTitle.Piece.BISHOP);
        CreatePiece(new Vector2(3, 0), isWhite, PieceTitle.Piece.QUEEN);
        CreatePiece(new Vector2(5, 0), isWhite, PieceTitle.Piece.BISHOP);
        CreatePiece(new Vector2(6, 0), isWhite, PieceTitle.Piece.KNIGHT);
        CreatePiece(new Vector2(7, 0), isWhite, PieceTitle.Piece.ROOK);
        for (int i = 0; i < 8; i++)
            CreatePiece(new Vector2(i, 1), isWhite, PieceTitle.Piece.PAWN);
        
        isWhite = false;
        CreatePiece(new Vector2(0, 7), isWhite, PieceTitle.Piece.ROOK);
        CreatePiece(new Vector2(1, 7), isWhite, PieceTitle.Piece.KNIGHT);
        CreatePiece(new Vector2(2, 7), isWhite, PieceTitle.Piece.BISHOP);
        CreatePiece(new Vector2(3, 7), isWhite, PieceTitle.Piece.QUEEN);
        CreatePiece(new Vector2(5, 7), isWhite, PieceTitle.Piece.BISHOP);
        CreatePiece(new Vector2(6, 7), isWhite, PieceTitle.Piece.KNIGHT);
        CreatePiece(new Vector2(7, 7), isWhite, PieceTitle.Piece.ROOK);
        for (int i = 0; i < 8; i++)
            CreatePiece(new Vector2(i, 6), isWhite, PieceTitle.Piece.PAWN);
        
        StartCoroutine(AddInfo(chessController));
    }

    void CreatePiece(Vector2 pos, bool isWhite, PieceTitle.Piece pieceType)
    {
        GameObject piece = Instantiate(piecePrefab, pieceParent);
        piece.name = string.Format("{0}{1}", isWhite ? "White" : "Black", pieceNames[(int)pieceType]);
        RectTransform rect = piece.GetComponent<RectTransform>();
        Vector2 position = new Vector2((pos.x * gridSpace) + gridOrigin, (pos.y * gridSpace) + gridOrigin);
        
        PieceController pieceController = piece.GetComponent<PieceController>();
        pieceController.gridCoordinate = new Vector2(pos.x, pos.y);
        pieceController.isWhite = isWhite;
        pieceController.piece = pieceType;
        pieceController.score = gameScores[(int)pieceType];
        piece.GetComponent<Image>().sprite = pieceSprites[(int)pieceType + (System.Enum.GetValues(typeof(PieceTitle.Piece)).Length * (isWhite?0:1))];
        rect.sizeDelta = Vector2.one * gridSpace;
        rect.localPosition = position;
        Debug.LogFormat("rect size delta: {0}, gridspace: {1}", rect.sizeDelta, gridSpace);
    }

    IEnumerator AddInfo(ChessController chessController)
    {
        yield return new WaitForSeconds(0.2f);
        chessController.AddToPieceInfo();
    }

    public int ReturnPieceValue(int piece)
    {
        return pieceScores[piece];
    }

    public void SetPieceValue(int piece, int value)
    {
        pieceScores[piece] = value;
        
    }

    public bool PointLimitPossible(int pointLimit)
    {
        float regularPieces = pieceScores[1] * 2 + pieceScores[2] * 2 + pieceScores[3] * 2;
        float fullQueenScore = pieceScores[0] * 9;
        float fullPawnScore = pieceScores[0] + pieceScores[4] * 8;
        return (pointLimit < (regularPieces + fullPawnScore) || pointLimit < (regularPieces + fullQueenScore));
    }
}