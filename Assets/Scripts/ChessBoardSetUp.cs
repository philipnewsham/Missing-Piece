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

    void Start()
    {
        ChessController chessController = GetComponent<ChessController>();
        gridSpace = chessController.gridSize;
        gridOrigin = chessController.gridOrigin.x;

        for (int i = 0; i < 8; i++)//white pawns
        {
            GameObject whitePawn = Instantiate(whitePawnPrefab, pieceParent);
            RectTransform wpRect = whitePawn.GetComponent<RectTransform>();
            Vector2 position = new Vector2((i * gridSpace) + gridOrigin, (1 * gridSpace) + gridOrigin);
            wpRect.localPosition = position;
            whitePawn.GetComponent<WhitePawn>().gridCoordinate = new Vector2(i, 1);
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
        }
        //end of white pieces
        //black pawns
        for (int i = 0; i < 8; i++)
        {
            GameObject blackPawn = Instantiate(blackPawnPrefab, pieceParent);
            RectTransform bpRect = blackPawn.GetComponent<RectTransform>();
            Vector2 position = new Vector2((i * gridSpace) + gridOrigin, (6 * gridSpace) + gridOrigin);
            bpRect.localPosition = position;
            blackPawn.GetComponent<BlackPawn>().gridCoordinate = new Vector2(i, 6);
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
