using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCapturedPiece : MonoBehaviour
{
    private List<GameObject> pieceImages = new List<GameObject>();
    private int pieceCount = 0;
    public Sprite[] sprites;
	
    void Start()
    {
        HidePieces();
    }

    public void HidePieces()
    {
        pieceImages.Clear();
        foreach (Transform child in transform)
        {
            pieceImages.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
    }

	public void ShowLatestPiece(PieceTitle.Piece piece)
    {
        pieceImages[pieceCount].SetActive(true);
        pieceImages[pieceCount].GetComponent<Image>().sprite = sprites[(int)piece];
        pieceCount++;
	}
}