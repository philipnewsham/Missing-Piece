using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Transform pieceParent;
    private float gridSpace;
    private float gridOrigin;

    public bool editedScores;
    [HideInInspector]
    public int[] pieceScores = new int[5] { 1, 5, 3, 3, 9 }; //pawn, rook, knight, bishop, queen

    private int[] gameScores;

    ChessController chessController;

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

        for (int i = 0; i < 8; i++)//white pawns
        {
            GameObject piece = Instantiate(whitePawnPrefab, pieceParent);
            RectTransform rect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2((i * gridSpace) + gridOrigin, (1 * gridSpace) + gridOrigin);
            rect.localPosition = position;
            piece.GetComponent<WhitePawn>().gridCoordinate = new Vector2(i, 1);
            piece.GetComponent<PieceInformation>().score = gameScores[0];
        }

        //white rooks
        for (int i = 0; i < 2; i++)
        {
            GameObject piece = Instantiate(whiteRookPrefab, pieceParent);
            RectTransform wrRect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2(((i*7) * gridSpace) + gridOrigin, (0 * gridSpace) + gridOrigin);
            wrRect.localPosition = position;
            piece.GetComponent<Rook>().gridCoordinate = new Vector2(i * 7, 0);
            piece.GetComponent<Rook>().isWhite = true;
            piece.GetComponent<PieceInformation>().score = gameScores[1];
        }

        //white knights
        for (int i = 0; i < 2; i++)
        {
            GameObject piece = Instantiate(whiteKnightPrefab, pieceParent);
            RectTransform rect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2(((1 + (i * 5)) * gridSpace) + gridOrigin, (0 * gridSpace) + gridOrigin);
            rect.localPosition = position;
            piece.GetComponent<Knight>().gridCoordinate = new Vector2(1 + (i * 5), 0);
            piece.GetComponent<Knight>().isWhite = true;
            piece.GetComponent<PieceInformation>().score = gameScores[2];
        }

        //white bishop
        for (int i = 0; i < 2; i++)
        {
            GameObject piece = Instantiate(whiteBishopPrefab, pieceParent);
            RectTransform rect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2(((2+(i * 3)) * gridSpace) + gridOrigin, (0 * gridSpace) + gridOrigin);
            rect.localPosition = position;
            piece.GetComponent<Bishop>().gridCoordinate = new Vector2(2 + (i * 3), 0);
            piece.GetComponent<Bishop>().isWhite = true;
            piece.GetComponent<PieceInformation>().score = gameScores[3];
        }
        //white queen
        for (int i = 0; i < 1; i++)
        {
            GameObject piece = Instantiate(whiteQueenPrefab, pieceParent);
            RectTransform rect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2((3 * gridSpace) + gridOrigin, 0 + gridOrigin);
            rect.localPosition = position;
            piece.GetComponent<Queen>().gridCoordinate = new Vector2(3, 0);
            piece.GetComponent<Queen>().isWhite = true;
            piece.GetComponent<PieceInformation>().score = gameScores[4];
        }
        //end of white pieces
        //black pawns
        for (int i = 0; i < 8; i++)
        {
            GameObject piece = Instantiate(blackPawnPrefab, pieceParent);
            RectTransform rect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2((i * gridSpace) + gridOrigin, (6 * gridSpace) + gridOrigin);
            rect.localPosition = position;
            piece.GetComponent<BlackPawn>().gridCoordinate = new Vector2(i, 6);
            piece.GetComponent<PieceInformation>().score = gameScores[0];
        }

        //black rooks
        for (int i = 0; i < 2; i++)
        {
            GameObject piece = Instantiate(blackRookPrefab, pieceParent);
            RectTransform rect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2(((i * 7) * gridSpace) + gridOrigin, (7 * gridSpace) + gridOrigin);
            rect.localPosition = position;
            piece.GetComponent<Rook>().gridCoordinate = new Vector2(i * 7, 7);
            piece.GetComponent<Rook>().isWhite = false;
            piece.GetComponent<PieceInformation>().score = gameScores[1];
        }

        //black knights
        for (int i = 0; i < 2; i++)
        {
            GameObject piece = Instantiate(blackKnightPrefab, pieceParent);
            RectTransform rect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2(((1 + (i * 5)) * gridSpace) + gridOrigin, (7 * gridSpace) + gridOrigin);
            rect.localPosition = position;
            piece.GetComponent<Knight>().gridCoordinate = new Vector2(1 + (i * 5), 7);
            piece.GetComponent<Knight>().isWhite = false;
            piece.GetComponent<PieceInformation>().score = gameScores[2];
        }

        //black bishop
        for (int i = 0; i < 2; i++)
        {
            GameObject piece = Instantiate(blackBishopPrefab, pieceParent);
            RectTransform rect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2(((2 + (i * 3)) * gridSpace) + gridOrigin, (7 * gridSpace) + gridOrigin);
            rect.localPosition = position;
            piece.GetComponent<Bishop>().gridCoordinate = new Vector2(2 + (i * 3), 7);
            piece.GetComponent<Bishop>().isWhite = false;
            piece.GetComponent<PieceInformation>().score = gameScores[3];
        }
        //black queen
        for (int i = 0; i < 1; i++)
        {
            GameObject piece = Instantiate(blackQueenPrefab, pieceParent);
            RectTransform rect = piece.GetComponent<RectTransform>();
            Vector2 position = new Vector2((3 * gridSpace) + gridOrigin, (7 * gridSpace) + gridOrigin);
            rect.localPosition = position;
            piece.GetComponent<Queen>().gridCoordinate = new Vector2(3, 7);
            piece.GetComponent<Queen>().isWhite = false;
            piece.GetComponent<PieceInformation>().score = gameScores[4];
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
