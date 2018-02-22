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

    public void AddToPieceInfo()
    {
        pieceInformations = FindObjectsOfType<PieceInformation>();
        foreach(PieceInformation info in pieceInformations)
        {
            if (info.isWhite)
            {
                whitePieces.Add(info.gameObject.GetComponent<Button>());
            }
            else
            {
                blackPieces.Add(info.gameObject.GetComponent<Button>());
            }
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
        Debug.LogFormat("{0}x, {1}y, {2}",space.x,space.y,pieceInfo);
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
        }
    }

    void UpdateScores()
    {
        whiteScoreText.text = string.Format("White: {0}", whiteScore);
        blackScoreText.text = string.Format("Black: {0}", blackScore);

        //check win conditions
        if (goalCapture)
        {

        }
        else if (goalScore)
        {

        }
    }

    bool isWhite;
    public void EnablePieces()
    {
        isWhite = !isWhite;

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
    }
}