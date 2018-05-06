using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessBoardSetUp : MonoBehaviour
{
    public GameObject whitePawnPrefab;
    public GameObject whiteRookPrefab;
    public GameObject whiteKnightPrefab;
    public GameObject whiteBishopPrefab;
    public GameObject whiteQueenPrefab;

    public GameObject blackPawnPrefab;
    public GameObject blackRookPrefab;
    public GameObject blackKnightPrefab;
    public GameObject blackBishopPrefab;
    public GameObject blackQueenPrefab;
    public GameObject piecePrefab;

    public Transform pieceParent;
    private float gridSpace;
    private float gridOrigin;

    public bool editedScores;
    [HideInInspector]
    public int[] pieceScores = new int[5] { 1, 5, 3, 3, 9 }; //pawn, rook, knight, bishop, queen

    private int[] gameScores;

    ChessController chessController;

    public Sprite[] pieceSprites;
    
    void Start()
    {
        chessController = GetComponent<ChessController>();
        gridSpace = chessController.gridSize;
        gridOrigin = chessController.gridOrigin.x;
    }

    public void SetUpGame()
    { 
        if (!editedScores)
        {
            gameScores = new int[5];
            gameScores = pieceScores;
        }
        //white pawns
        for (int i = 0; i < 8; i++)
        {
            GameObject piece = Instantiate(piecePrefab, pieceParent);
            RectTransform rect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2((i * gridSpace) + gridOrigin, (1 * gridSpace) + gridOrigin);
            rect.localPosition = position;
            piece.GetComponent<PieceController>().gridCoordinate = new Vector2(i, 1);
            piece.GetComponent<PieceController>().isWhite = true;
            piece.GetComponent<PieceController>().piece = PieceTitle.Piece.PAWN;
            piece.GetComponent<PieceInformation>().score = gameScores[0];
            piece.GetComponent<Image>().sprite = pieceSprites[(int)PieceTitle.Piece.PAWN];
        }
        //white rooks
        for (int i = 0; i < 2; i++)
        {
            GameObject piece = Instantiate(piecePrefab, pieceParent);
            RectTransform rect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2(((i * 7) * gridSpace) + gridOrigin, (0 * gridSpace) + gridOrigin);
            rect.localPosition = position;
            piece.GetComponent<PieceController>().gridCoordinate = new Vector2(i * 7, 0);
            piece.GetComponent<PieceController>().isWhite = true;
            piece.GetComponent<PieceController>().piece = PieceTitle.Piece.ROOK;
            piece.GetComponent<PieceInformation>().score = gameScores[1];
            piece.GetComponent<Image>().sprite = pieceSprites[(int)PieceTitle.Piece.ROOK];
        }
        //white knights
        for (int i = 0; i < 2; i++)
        {
            GameObject piece = Instantiate(piecePrefab, pieceParent);
            RectTransform rect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2(((1 + (i * 5)) * gridSpace) + gridOrigin, (0 * gridSpace) + gridOrigin);
            rect.localPosition = position;
            piece.GetComponent<PieceController>().gridCoordinate = new Vector2(1 + (i * 5), 0);
            piece.GetComponent<PieceController>().isWhite = true;
            piece.GetComponent<PieceController>().piece = PieceTitle.Piece.KNIGHT;
            piece.GetComponent<PieceInformation>().score = gameScores[2];
            piece.GetComponent<Image>().sprite = pieceSprites[(int)PieceTitle.Piece.KNIGHT];
        }
        //white bishops
        for (int i = 0; i < 2; i++)
        {
            GameObject piece = Instantiate(piecePrefab, pieceParent);
            RectTransform rect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2(((2 + (i * 3)) * gridSpace) + gridOrigin, (0 * gridSpace) + gridOrigin);
            rect.localPosition = position;
            piece.GetComponent<PieceController>().gridCoordinate = new Vector2(2 + (i * 3), 0);
            piece.GetComponent<PieceController>().isWhite = true;
            piece.GetComponent<PieceController>().piece = PieceTitle.Piece.BISHOP;
            piece.GetComponent<PieceInformation>().score = gameScores[3];
            piece.GetComponent<Image>().sprite = pieceSprites[(int)PieceTitle.Piece.BISHOP];
        }

        //white Queen
        for (int i = 0; i < 1; i++)
        {
            GameObject piece = Instantiate(piecePrefab, pieceParent);
            RectTransform rect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2((3 * gridSpace) + gridOrigin, 0 + gridOrigin);
            rect.localPosition = position;
            piece.GetComponent<PieceController>().gridCoordinate = new Vector2(3, 0);
            piece.GetComponent<PieceController>().isWhite = true;
            piece.GetComponent<PieceController>().piece = PieceTitle.Piece.QUEEN;
            piece.GetComponent<PieceInformation>().score = gameScores[4];
            piece.GetComponent<Image>().sprite = pieceSprites[(int)PieceTitle.Piece.QUEEN];
        }

        //end of white pieces
        //black pawns
        for (int i = 0; i < 8; i++)
        {
            GameObject piece = Instantiate(piecePrefab, pieceParent);
            RectTransform rect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2((i * gridSpace) + gridOrigin, (6 * gridSpace) + gridOrigin);
            rect.localPosition = position;
            piece.GetComponent<PieceController>().gridCoordinate = new Vector2(i, 6);
            piece.GetComponent<PieceController>().isWhite = false;
            piece.GetComponent<PieceController>().piece = PieceTitle.Piece.PAWN;
            piece.GetComponent<PieceInformation>().score = gameScores[0];
            piece.GetComponent<Image>().sprite = pieceSprites[(int)PieceTitle.Piece.PAWN + System.Enum.GetValues(typeof(PieceTitle.Piece)).Length];
        }

        for (int i = 0; i < 2; i++)
        {
            GameObject piece = Instantiate(piecePrefab, pieceParent);
            RectTransform rect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2(((i * 7) * gridSpace) + gridOrigin, (7 * gridSpace) + gridOrigin);
            rect.localPosition = position;
            piece.GetComponent<PieceController>().gridCoordinate = new Vector2(i * 7, 7);
            piece.GetComponent<PieceController>().isWhite = false;
            piece.GetComponent<PieceController>().piece = PieceTitle.Piece.ROOK;
            piece.GetComponent<PieceInformation>().score = gameScores[1];
            piece.GetComponent<Image>().sprite = pieceSprites[(int)PieceTitle.Piece.ROOK + System.Enum.GetValues(typeof(PieceTitle.Piece)).Length];
        }

        for (int i = 0; i < 2; i++)
        {
            GameObject piece = Instantiate(piecePrefab, pieceParent);
            RectTransform rect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2(((1 + (i * 5)) * gridSpace) + gridOrigin, (7 * gridSpace) + gridOrigin);
            rect.localPosition = position;
            piece.GetComponent<PieceController>().gridCoordinate = new Vector2(1 + (i * 5), 7);
            piece.GetComponent<PieceController>().isWhite = false;
            piece.GetComponent<PieceController>().piece = PieceTitle.Piece.KNIGHT;
            piece.GetComponent<PieceInformation>().score = gameScores[2];
            piece.GetComponent<Image>().sprite = pieceSprites[(int)PieceTitle.Piece.KNIGHT + System.Enum.GetValues(typeof(PieceTitle.Piece)).Length];
        }

        for (int i = 0; i < 2; i++)
        {
            GameObject piece = Instantiate(piecePrefab, pieceParent);
            RectTransform rect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2(((2 + (i * 3)) * gridSpace) + gridOrigin, (7 * gridSpace) + gridOrigin);
            rect.localPosition = position;
            piece.GetComponent<PieceController>().gridCoordinate = new Vector2(2 + (i * 3), 7);
            piece.GetComponent<PieceController>().isWhite = false;
            piece.GetComponent<PieceController>().piece = PieceTitle.Piece.BISHOP;
            piece.GetComponent<PieceInformation>().score = gameScores[3];
            piece.GetComponent<Image>().sprite = pieceSprites[(int)PieceTitle.Piece.BISHOP + System.Enum.GetValues(typeof(PieceTitle.Piece)).Length];
        }

        for (int i = 0; i < 1; i++)
        {
            GameObject piece = Instantiate(piecePrefab, pieceParent);
            RectTransform rect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2((3 * gridSpace) + gridOrigin, (7 * gridSpace) + gridOrigin);
            rect.localPosition = position;
            piece.GetComponent<PieceController>().gridCoordinate = new Vector2(3, 7);
            piece.GetComponent<PieceController>().isWhite = false;
            piece.GetComponent<PieceController>().piece = PieceTitle.Piece.QUEEN;
            piece.GetComponent<PieceInformation>().score = gameScores[4];
            piece.GetComponent<Image>().sprite = pieceSprites[(int)PieceTitle.Piece.QUEEN + System.Enum.GetValues(typeof(PieceTitle.Piece)).Length];
        }
        //end of black pieces

        StartCoroutine(AddInfo(chessController));
    }

    IEnumerator AddInfo(ChessController chessController)
    {
        yield return new WaitForSeconds(0.2f);
        chessController.AddToPieceInfo();
    }
}
