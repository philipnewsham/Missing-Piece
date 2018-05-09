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
    public float gridSize;
    public Vector2 gridOrigin;

    private List<Button> whitePieces = new List<Button>();
    private List<Button> blackPieces = new List<Button>();

    private int blackScore;
    private int whiteScore;

    public Text whiteScoreText;
    public Text blackScoreText;

    public bool goalScore;
    private int score;

    public bool goalHighestTime;
    private int minutes;

    public bool goalHightestMove;
    private int moves;

    public bool goalCapture;
    private int capturePiece;

    private int turnAmount;
    private int currentTurn = -1;
    public Text currentTurnText;

    public Text playerTurnText;

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
        EnablePieces();
    }

    public PieceController CheckPieceOnSquare(Vector2 space)
    {
        return ReturnPieceController(space);
    }

    public void TakePiece(Vector2 space)
    {
        PieceController pieceInfo = ReturnPieceController(space);

        if (pieceInfo != null)
        {
            if (pieceInfo.isWhite) blackScore += pieceInfo.score;
            else whiteScore += pieceInfo.score;
            Destroy(pieceInfo.gameObject);
            UpdateScores();
            CheckPiecesRemaining();
        }
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
        //victoryText.color = Color.black;
        Debug.Log("black wins");
    }

    void WhiteWins()
    {
        victoryScreen.SetActive(true);
        victoryText.text = "White Wins!";
        //victoryText.color = Color.white;
        Debug.Log("white wins");
    }

    void TieGame()
    {
        victoryScreen.SetActive(true);
        victoryText.text = "It's a tie!";
        //victoryText.color = Color.grey;
        Debug.Log("It's a tie!");
    }

    bool isWhite;
    public void EnablePieces()
    {
        isWhite = !isWhite;

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
        if(currentTurn == turnAmount && turnAmount > 0)
        {
            currentTurnText.text = "";
            playerTurnText.text = "";
            if (whiteScore > blackScore)
                WhiteWins();
            else if (whiteScore < blackScore)
                BlackWins();
            else
                TieGame();

            GameFinished();
        }
    }

    void GameFinished()
    {
        Debug.Log("game ends");
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
        currentTurn = -2;
        whiteScore = 0;
        blackScore = 0;
        pieceInformations = null;
        UpdateScores();
        currentTurnText.text = "Turn: 0";
    }

    public void ClearAllPieces()
    {
        foreach (PieceController info in pieceInformations)
        {
            if (info != null)
                Destroy(info.gameObject);
        }
    }
}