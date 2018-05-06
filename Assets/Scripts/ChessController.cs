using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessController : MonoBehaviour
{
    private PieceInformation[] pieceInformations;
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

    public Text playerTurnText;

    public void AddToPieceInfo()
    {
        pieceInformations = FindObjectsOfType<PieceInformation>();
        foreach(PieceInformation info in pieceInformations)
        {
            if (info.isWhite)
                whitePieces.Add(info.gameObject.GetComponent<Button>());
            else
                blackPieces.Add(info.gameObject.GetComponent<Button>());
        }
        EnablePieces();
    }

    public PieceInformation CheckPieceOnSquare(Vector2 space)
    {
        PieceInformation pieceInfo = null;
        foreach(PieceInformation info in pieceInformations)
        {
            if(info.gridCoordinate == space)
            {
                pieceInfo = info;
            }
        }
        return pieceInfo;
    }

    public void TakePiece(Vector2 space)
    {
        PieceInformation pieceInfo = null;
        foreach (PieceInformation info in pieceInformations)
        {
            if (info.gridCoordinate == space)
            {
                pieceInfo = info;
            }
        }

        if (pieceInfo != null)
        {
            if (pieceInfo.isWhite) blackScore += pieceInfo.score;
            else whiteScore += pieceInfo.score;
            Destroy(pieceInfo.gameObject);
            UpdateScores();
            CheckPiecesRemaining();
        }
    }

    void CheckPiecesRemaining()
    {
        int whitePieceCount = 0;
        int blackPieceCount = 0;
        foreach (PieceInformation info in pieceInformations)
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

        //check win conditions
        if (goalCapture)
        {

        }
        else if (goalScore || score > 0)
            CheckForWinner();
    }

    void CheckForWinner()
    {
        if (whiteScore >= score)
            WhiteWins();
        else if (blackScore >= score)
            BlackWins();
    }

    void BlackWins()
    {
        Debug.Log("black wins");
    }

    void WhiteWins()
    {
        Debug.Log("white wins");
    }

    void TieGame()
    {
        Debug.Log("It's a tie!");
    }

    bool isWhite;
    public void EnablePieces()
    {
        isWhite = !isWhite;

        playerTurnText.text = string.Format("{0}'s turn",isWhite?"White":"Black");

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
        turnAmount = turns*2;
    }

    public void ChangePointAmount(int points)
    {
        score = points;
    }
}