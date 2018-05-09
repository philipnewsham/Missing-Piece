using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteBishop : MonoBehaviour
{
    public Vector2 gridCoordinate;
    private RectTransform rectTransform;
    private ChessController chessController;
    private PieceController thisInformation;
    private bool showMoves = false;
    private List<GameObject> moveButtons = new List<GameObject>();
    public GameObject moveButton;
    private float gridSize;
    private Vector2 gridOrigin;
    public GameObject dismissButton;
    private GameObject dismissButtonClone;
    public bool isWhite;

    void Start()
    {
        chessController = FindObjectOfType<ChessController>();
        gridSize = chessController.gridSize;
        gridOrigin = chessController.gridOrigin;
        rectTransform = GetComponent<RectTransform>();
        GetComponent<Button>().onClick.AddListener(() => ShowMoves());
        thisInformation = GetComponent<PieceController>();
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
            PieceController checkInfo = null;

            //checking diagonal UR
            if(gridCoordinate.x < 7 && gridCoordinate.y < 7)
            {
                bool isFinished = false;
                for (int i = 1; i < 8; i++)
                {
                    if (!isFinished)
                    {
                        checkSpace = new Vector2(gridCoordinate.x + i, gridCoordinate.y + i);
                        checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                        if(gridCoordinate.x + i > 7 || gridCoordinate.y + i > 7 || (checkInfo!=null && checkInfo.isWhite == thisInformation.isWhite))
                        {
                            isFinished = true;
                        }
                        else if(checkInfo == null)
                        {
                            possibleMoves.Add(checkSpace);
                        }
                        else if(checkInfo.isWhite == !thisInformation.isWhite)
                        {
                            possibleMoves.Add(checkSpace);
                            isFinished = true;
                        }
                    }
                }
            }

            //checking diagonal UL
            if (gridCoordinate.x > 0 && gridCoordinate.y < 7)
            {
                bool isFinished = false;
                for (int i = 1; i < 8; i++)
                {
                    if (!isFinished)
                    {
                        checkSpace = new Vector2(gridCoordinate.x - i, gridCoordinate.y + i);
                        checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                        if (gridCoordinate.x - i < 0 || gridCoordinate.y + i > 7 || (checkInfo != null && checkInfo.isWhite == thisInformation.isWhite))
                        {
                            isFinished = true;
                        }
                        else if (checkInfo == null)
                        {
                            possibleMoves.Add(checkSpace);
                        }
                        else if (checkInfo.isWhite == !thisInformation.isWhite)
                        {
                            possibleMoves.Add(checkSpace);
                            isFinished = true;
                        }
                    }
                }
            }

            //checking diagonal DR
            if (gridCoordinate.x < 7 && gridCoordinate.y > 0)
            {
                bool isFinished = false;
                for (int i = 1; i < 8; i++)
                {
                    if (!isFinished)
                    {
                        checkSpace = new Vector2(gridCoordinate.x + i, gridCoordinate.y - i);
                        checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                        if (gridCoordinate.x + i > 7 || gridCoordinate.y - i < 0 || (checkInfo != null && checkInfo.isWhite == thisInformation.isWhite))
                        {
                            isFinished = true;
                        }
                        else if (checkInfo == null)
                        {
                            possibleMoves.Add(checkSpace);
                        }
                        else if (checkInfo.isWhite == !thisInformation.isWhite)
                        {
                            possibleMoves.Add(checkSpace);
                            isFinished = true;
                        }
                    }
                }
            }

            //checking diagonal DL
            if (gridCoordinate.x > 0 && gridCoordinate.y > 0)
            {
                bool isFinished = false;
                for (int i = 1; i < 8; i++)
                {
                    if (!isFinished)
                    {
                        checkSpace = new Vector2(gridCoordinate.x - i, gridCoordinate.y - i);
                        checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                        if (gridCoordinate.x - i < 0 || gridCoordinate.y - i < 0 || (checkInfo != null && checkInfo.isWhite == thisInformation.isWhite))
                        {
                            isFinished = true;
                        }
                        else if (checkInfo == null)
                        {
                            possibleMoves.Add(checkSpace);
                        }
                        else if (checkInfo.isWhite == !thisInformation.isWhite)
                        {
                            possibleMoves.Add(checkSpace);
                            isFinished = true;
                        }
                    }
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
        Vector2 movePos = new Vector2((moveCoordinate.x * gridSize) + gridOrigin.x, (moveCoordinate.y * gridSize) + gridOrigin.y); //selects move pos
        rectTransform.localPosition = movePos; //moves piece
        chessController.TakePiece(moveCoordinate); //asks controller to remove any piece landed on
        gridCoordinate = moveCoordinate; //updates grid coordinate
        thisInformation.gridCoordinate = moveCoordinate; //updates piece information grid coordinate
    }
}
