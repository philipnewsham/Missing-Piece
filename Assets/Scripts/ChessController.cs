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
            Destroy(pieceInfo.gameObject);
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