using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Queen : MonoBehaviour
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
    public bool isWhite;
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

            //checking diagonal UR
            if (gridCoordinate.x < 7 && gridCoordinate.y < 7)
            {
                bool isFinished = false;
                for (int i = 1; i < 8; i++)
                {
                    if (!isFinished)
                    {
                        checkSpace = new Vector2(gridCoordinate.x + i, gridCoordinate.y + i);
                        checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                        if (gridCoordinate.x + i > 7 || gridCoordinate.y + i > 7 || (checkInfo != null && checkInfo.isWhite == thisInformation.isWhite))
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
            //checking down line
            if (gridCoordinate.y > 0)
            {
                bool isFinished = false;
                for (int i = 1; i < 8; i++)
                {
                    if (!isFinished)
                    {
                        checkSpace = new Vector2(gridCoordinate.x, gridCoordinate.y - i);
                        checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                        if (gridCoordinate.y - i < 0)//else space is not on board
                        {
                            isFinished = true;
                        }
                        else if (checkInfo == null) //if space is empty
                        {
                            possibleMoves.Add(checkSpace);
                        }
                        else if (checkInfo.isWhite == !thisInformation.isWhite) //else if space has an enemy
                        {
                            possibleMoves.Add(checkSpace);
                            isFinished = true;
                        }
                        else if (checkInfo.isWhite == thisInformation.isWhite)//else space has a friendly
                        {
                            isFinished = true;
                        }
                    }
                }
            }

            //checking up line
            if (gridCoordinate.y < 7)
            {
                bool isFinished = false;
                for (int i = 1; i < 8; i++)
                {
                    if (!isFinished)
                    {
                        checkSpace = new Vector2(gridCoordinate.x, gridCoordinate.y + i);
                        checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                        if (gridCoordinate.y + i > 7)//else space is not on board
                        {
                            isFinished = true;
                        }
                        else if (checkInfo == null) //if space is empty
                        {
                            possibleMoves.Add(checkSpace);
                        }
                        else if (checkInfo.isWhite == !thisInformation.isWhite) //else if space has an enemy
                        {
                            possibleMoves.Add(checkSpace);
                            isFinished = true;
                        }
                        else if (checkInfo.isWhite == thisInformation.isWhite)//else space has a friendly
                        {
                            isFinished = true;
                        }
                    }
                }
            }

            //checking right line
            if (gridCoordinate.x > 0)
            {
                bool isFinished = false;
                for (int i = 1; i < 8; i++)
                {
                    if (!isFinished)
                    {
                        checkSpace = new Vector2(gridCoordinate.x - i, gridCoordinate.y);
                        checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                        if (gridCoordinate.x - i < 0)//else space is not on board
                        {
                            isFinished = true;
                        }
                        else if (checkInfo == null) //if space is empty
                        {
                            possibleMoves.Add(checkSpace);
                        }
                        else if (checkInfo.isWhite == !thisInformation.isWhite) //else if space has an enemy
                        {
                            possibleMoves.Add(checkSpace);
                            isFinished = true;
                        }
                        else if (checkInfo.isWhite == thisInformation.isWhite)//else space has a friendly
                        {
                            isFinished = true;
                        }
                    }
                }
            }
            //checking right line
            if (gridCoordinate.x < 7)
            {
                bool isFinished = false;
                for (int i = 1; i < 8; i++)
                {
                    if (!isFinished)
                    {
                        checkSpace = new Vector2(gridCoordinate.x + i, gridCoordinate.y);
                        checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                        if (gridCoordinate.x + i > 7)//else space is not on board
                        {
                            isFinished = true;
                        }
                        else if (checkInfo == null) //if space is empty
                        {
                            possibleMoves.Add(checkSpace);
                        }
                        else if (checkInfo.isWhite == !thisInformation.isWhite) //else if space has an enemy
                        {
                            possibleMoves.Add(checkSpace);
                            isFinished = true;
                        }
                        else if (checkInfo.isWhite == thisInformation.isWhite)//else space has a friendly
                        {
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
        chessController.EnablePieces();
        audioSource.Play();
    }
}