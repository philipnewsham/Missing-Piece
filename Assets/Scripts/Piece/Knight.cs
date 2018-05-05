using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knight : MonoBehaviour
{
    public Vector2 gridCoordinate;
    private RectTransform rectTransform;
    private ChessController chessController;
    private PieceInformation thisInformation;
    private bool showMoves = false;
    private List<GameObject> moveButtons = new List<GameObject>();
    public GameObject moveButton;
    private float gridSize;
    private Vector2 gridOrigin;
    public GameObject dismissButton;
    private GameObject dismissButtonClone;
    public bool isWhite = false;
    private AudioSource audioSource;

    void Start()
    {
        chessController = FindObjectOfType<ChessController>();
        gridSize = chessController.gridSize;
        gridOrigin = chessController.gridOrigin;
        rectTransform = GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();
        GetComponent<Button>().onClick.AddListener(() => { ShowMoves(); audioSource.Play(); });
        thisInformation = GetComponent<PieceInformation>();
        thisInformation.gridCoordinate = gridCoordinate;
        thisInformation.isWhite = isWhite;
        thisInformation.isKing = false;
    }

    void ShowMoves()
    {
        showMoves = !showMoves;
        if (showMoves)
        {
            List<Vector2> possibleMoves = new List<Vector2>();
            Vector2 checkSpace = new Vector2();
            PieceInformation checkInfo = null;

            //checking UL
            if(gridCoordinate.x - 1 >= 0 && gridCoordinate.y + 2 < 8)
            {
                checkSpace = new Vector2(gridCoordinate.x - 1, gridCoordinate.y + 2);
                checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                if(checkInfo == null || checkInfo.isWhite == !thisInformation.isWhite)
                {
                    possibleMoves.Add(checkSpace);
                }
            }

            //checking UR
            if (gridCoordinate.x + 1 < 8 && gridCoordinate.y + 2 < 8)
            {
                checkSpace = new Vector2(gridCoordinate.x + 1, gridCoordinate.y + 2);
                checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                if (checkInfo == null || checkInfo.isWhite == !thisInformation.isWhite)
                {
                    possibleMoves.Add(checkSpace);
                }
            }

            //checking UL
            if (gridCoordinate.x - 1 >= 0 && gridCoordinate.y + 2 < 8)
            {
                checkSpace = new Vector2(gridCoordinate.x - 1, gridCoordinate.y + 2);
                checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                if (checkInfo == null || checkInfo.isWhite == !thisInformation.isWhite)
                {
                    possibleMoves.Add(checkSpace);
                }
            }

            //checking RU
            if (gridCoordinate.x + 2 < 8 && gridCoordinate.y + 1 < 8)
            {
                checkSpace = new Vector2(gridCoordinate.x + 2, gridCoordinate.y + 1);
                checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                if (checkInfo == null || checkInfo.isWhite == !thisInformation.isWhite)
                {
                    possibleMoves.Add(checkSpace);
                }
            }

            //checking RD
            if (gridCoordinate.x + 2 < 8 && gridCoordinate.y - 1 >= 0)
            {
                checkSpace = new Vector2(gridCoordinate.x + 2, gridCoordinate.y - 1);
                checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                if (checkInfo == null || checkInfo.isWhite == !thisInformation.isWhite)
                {
                    possibleMoves.Add(checkSpace);
                }
            }

            //checking RU
            if (gridCoordinate.x + 2 < 8 && gridCoordinate.y + 1 < 8)
            {
                checkSpace = new Vector2(gridCoordinate.x + 2, gridCoordinate.y + 1);
                checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                if (checkInfo == null || checkInfo.isWhite == !thisInformation.isWhite)
                {
                    possibleMoves.Add(checkSpace);
                }
            }

            //checking DR
            if (gridCoordinate.x + 1 < 8 && gridCoordinate.y - 2 >= 0)
            {
                checkSpace = new Vector2(gridCoordinate.x + 1, gridCoordinate.y - 2);
                checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                if (checkInfo == null || checkInfo.isWhite == !thisInformation.isWhite)
                {
                    possibleMoves.Add(checkSpace);
                }
            }

            //checking DR
            if (gridCoordinate.x + 1 < 8 && gridCoordinate.y - 2 >= 0)
            {
                checkSpace = new Vector2(gridCoordinate.x + 1, gridCoordinate.y - 2);
                checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                if (checkInfo == null || checkInfo.isWhite == !thisInformation.isWhite)
                {
                    possibleMoves.Add(checkSpace);
                }
            }

            //checking DL
            if (gridCoordinate.x - 1 >= 0 && gridCoordinate.y - 2 >= 0)
            {
                checkSpace = new Vector2(gridCoordinate.x - 1, gridCoordinate.y - 2);
                checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                if (checkInfo == null || checkInfo.isWhite == !thisInformation.isWhite)
                {
                    possibleMoves.Add(checkSpace);
                }
            }

            //checking LD
            if (gridCoordinate.x - 2 >= 0 && gridCoordinate.y - 1 >= 0)
            {
                checkSpace = new Vector2(gridCoordinate.x - 2, gridCoordinate.y - 1);
                checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                if (checkInfo == null || checkInfo.isWhite == !thisInformation.isWhite)
                {
                    possibleMoves.Add(checkSpace);
                }
            }

            //checking RU
            if (gridCoordinate.x - 2 >= 0 && gridCoordinate.y + 1 < 8)
            {
                checkSpace = new Vector2(gridCoordinate.x - 2, gridCoordinate.y + 1);
                checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                if (checkInfo == null || checkInfo.isWhite == !thisInformation.isWhite)
                {
                    possibleMoves.Add(checkSpace);
                }
            }

            GameObject dismissClone = Instantiate(dismissButton, transform.parent);
            dismissClone.GetComponent<Button>().onClick.AddListener(() => ShowMoves());
            dismissButtonClone = dismissClone;
            if (possibleMoves.Count > 0)
            {
                for (int i = 0; i < possibleMoves.Count; i++)//spawning buttons
                {
                    GameObject buttonClone = Instantiate(moveButton, transform.parent);
                    RectTransform rect = buttonClone.GetComponent<RectTransform>();
                    Vector2 move = new Vector2((possibleMoves[i].x * gridSize) + gridOrigin.x, (possibleMoves[i].y * gridSize) + gridOrigin.y);
                    rect.localPosition = move;
                    moveButtons.Add(buttonClone);
                    Vector2 moveTo = new Vector2(possibleMoves[i].x, possibleMoves[i].y);
                    Button button = buttonClone.GetComponent<Button>();
                    button.onClick.AddListener(() => MovePiece(moveTo));
                }
            }
        }
        else //Destroy move buttons
        {
            for (int i = 0; i < moveButtons.Count; i++)
            {
                Destroy(moveButtons[i]);
            }
            moveButtons.Clear();
            Destroy(dismissButtonClone);
        }
    }
    

    void MovePiece(Vector2 moveCoordinate)//moving the piece
    {
        ShowMoves();//this hides the move buttons
        Debug.Log("move piece");
        Vector2 movePos = new Vector2((moveCoordinate.x * gridSize) + gridOrigin.x, (moveCoordinate.y * gridSize) + gridOrigin.y); //selects move pos
        rectTransform.localPosition = movePos; //moves piece
        chessController.TakePiece(moveCoordinate); //asks controller to remove any piece landed on
        gridCoordinate = moveCoordinate; //updates grid coordinate
        thisInformation.gridCoordinate = moveCoordinate; //updates piece information grid coordinate
        chessController.EnablePieces();
        audioSource.Play();
    }
}