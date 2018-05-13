using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceTitle
{
    public enum Piece
    {
        QUEEN,
        BISHOP,
        KNIGHT,
        ROOK,
        PAWN
    }
}

public class ChessController : MonoBehaviour
{
    private PieceController[] pieceInformations;
    private List<PieceController> enPassantMoves = new List<PieceController>();
    public GridLayoutGroup chessBoardLayout;
    private Vector2 gridOrigin;

    private List<Button> whitePieces = new List<Button>();
    private List<Button> blackPieces = new List<Button>();

    private int blackScore;
    private int whiteScore;

    public Text whiteScoreText;
    public Text blackScoreText;

    public bool goalScore;
    private int score;
    
    public bool goalCapture;

    private int turnAmount;
    private int currentTurn = -1;
    public Text currentTurnText;

    public Text playerTurnText;

    public PieceTitle.Piece capturePiece;

    public ShowCapturedPiece[] showCapturedPieces;

    private ShowMoves showMoves;

    void Start()
    {
        showMoves = FindObjectOfType<ShowMoves>();
    }

    public void AddToPieceInfo()
    {
        pieceInformations = FindObjectsOfType<PieceController>();
        foreach(PieceController info in pieceInformations)
        {
            if (info.isWhite)
                whitePieces.Add(info.gameObject.GetComponent<Button>());
            else
                blackPieces.Add(info.gameObject.GetComponent<Button>());
        }
        isWhite = false;
        EnablePieces();
    }

    public PieceController CheckPieceOnSquare(Vector2 space)
    {
        return ReturnPieceController(space);
    }

    public void TakePiece(Vector2 space, PieceTitle.Piece piece)
    {
        PieceController pieceInfo = ReturnPieceController(space);
        if (pieceInfo == null && piece == PieceTitle.Piece.PAWN)
            pieceInfo = CheckEnPassant(space);

        if (pieceInfo != null)
        {
            if (pieceInfo.isWhite) { blackScore += pieceInfo.score; showCapturedPieces[0].ShowLatestPiece(pieceInfo.piece); }
            else { whiteScore += pieceInfo.score; showCapturedPieces[1].ShowLatestPiece(pieceInfo.piece); }

            if(goalCapture && pieceInfo.piece == capturePiece)
            {
                if (pieceInfo.isWhite)
                    BlackWins();
                else
                    WhiteWins();
            }

            Destroy(pieceInfo.gameObject);
            UpdateScores();
            CheckPiecesRemaining();
        }
    }

    public PieceController CheckEnPassant(Vector2 position)
    {
        PieceController pieceInfo = null;
        if(enPassantMoves.Count > 0)
        {
            foreach (PieceController info in enPassantMoves)
            {
                Vector2 pos = new Vector2(info.gridCoordinate.x, info.gridCoordinate.y + (info.isWhite ? -1 : 1));
                if (pos == position)
                    pieceInfo = info;
            }
        }
        return pieceInfo;
    }

    PieceController ReturnPieceController(Vector2 position)
    {
        PieceController pieceInfo = null;
        foreach (PieceController info in pieceInformations)
        {
            if (info != null && info.gridCoordinate == position)
                pieceInfo = info;
        }
        return pieceInfo;
    }

    void CheckPiecesRemaining()
    {
        int whitePieceCount = 0;
        int blackPieceCount = 0;
        foreach (PieceController info in pieceInformations)
        {
            if (info != null)
            { 
                if (info.isWhite)
                    whitePieceCount++;
                else
                    blackPieceCount++;
            }
        }

        if (whitePieceCount == 1)
            BlackWins();
        if (blackPieceCount == 1)
            WhiteWins();
    }

    void UpdateScores()
    {
        whiteScoreText.text = string.Format("White: {0}", whiteScore);
        blackScoreText.text = string.Format("Black: {0}", blackScore);

        if (goalScore || score > 0)
            CheckForWinner();
    }

    void CheckForWinner()
    {
        if (whiteScore >= score)
            WhiteWins();
        else if (blackScore >= score)
            BlackWins();
    }

    public GameObject victoryScreen;
    public Text victoryText;

    void BlackWins()
    {
        victoryScreen.SetActive(true);
        victoryText.text = "Black Wins!";
    }

    void WhiteWins()
    {
        victoryScreen.SetActive(true);
        victoryText.text = "White Wins!";
    }

    void TieGame()
    {
        victoryScreen.SetActive(true);
        victoryText.text = "It's a tie!";
    }

    void Stalemate()
    {
        victoryScreen.SetActive(true);
        victoryText.text = "Stalemate!";
    }

    bool isWhite;
    public void EnablePieces()
    {
        isWhite = !isWhite;
        Debug.LogFormat("is stalemate = {0}", IsStalemate());
        if (IsStalemate())
            Stalemate();

        playerTurnText.text = string.Format("{0}'s turn",isWhite?"White":"Black");
        currentTurnText.text = string.Format("Turn: {0}", currentTurn+2);
        for (int i = 0; i < whitePieces.Count; i++)
        {
            if(whitePieces[i] != null)
                whitePieces[i].interactable = isWhite;
        }

        for (int i = 0; i < blackPieces.Count; i++)
        {
            if(blackPieces[i]!=null)
                blackPieces[i].interactable = !isWhite;
        }
        currentTurn++;
        RemoveEnPassantMoves();
        if(goalScore && currentTurn == turnAmount && turnAmount > 0)
        {
            currentTurnText.text = "";
            playerTurnText.text = "";
            if (whiteScore > blackScore)
                WhiteWins();
            else if (whiteScore < blackScore)
                BlackWins();
            else
                TieGame();
        }
    }

    void RemoveEnPassantMoves()
    {
        List<PieceController> removePieces = new List<PieceController>();
        foreach(PieceController pieceInfo in enPassantMoves)
        {
            if (pieceInfo.isWhite == isWhite)
                removePieces.Add(pieceInfo);
        }

        if (removePieces.Count > 0)
        {
            for (int i = 0; i < removePieces.Count; i++)
                enPassantMoves.Remove(removePieces[i]);
        }
    }

    public void ChangeTurnAmount(int turns)
    {
        turnAmount = turns;
    }

    public void ChangePointAmount(int points)
    {
        score = points;
    }

    public void Reset()
    {
        ClearAllPieces();
        currentTurn = -1;
        whiteScore = 0;
        blackScore = 0;
        pieceInformations = null;
        UpdateScores();
        currentTurnText.text = "Turn: 0";
        isWhite = false;
    }

    public void ClearAllPieces()
    {
        foreach (PieceController info in pieceInformations)
        {
            if (info != null)
                Destroy(info.gameObject);
        }
    }

    public void AddEnPassant(PieceController pieceController)
    {
        enPassantMoves.Add(pieceController);
    }

    public float ReturnGridSize()
    {
        return chessBoardLayout.cellSize.x;
    }

    public Vector2 ReturnOrigin()
    {
        float coordinate = (ReturnGridSize() / 2.0f) - (chessBoardLayout.GetComponent<RectTransform>().sizeDelta.x / 2.0f);
        return Vector2.one * coordinate;
    }

    bool IsStalemate()
    {
        //checking all pieces in game
        foreach(PieceController piece in pieceInformations)
        {
            if(piece != null && piece.isWhite == isWhite)
            {
                List<Vector2> moves = new List<Vector2>();
                moves = showMoves.ShowPossibleMoves(piece.gridCoordinate, piece.isWhite, piece.piece, piece.firstMove);
                if (moves.Count > 0)//if a move is possible, then it's not stalemate
                    return false;
            }
        }
        return true;//if all pieces of current colour has been checked and no moves have been found, it's stalemate
    }
}