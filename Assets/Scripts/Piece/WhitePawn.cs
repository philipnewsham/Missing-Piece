using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhitePawn : MonoBehaviour
{
    public Vector2 gridCoordinate;
    private RectTransform rectTransform;
    private bool firstMove = true;
    private ChessController chessController;
    private PieceInformation thisInformation;
    private bool showMoves = false;
    private List<GameObject> moveButtons = new List<GameObject>();
    public GameObject moveButton;
    private float gridSize;
    private Vector2 gridOrigin;
    public GameObject dismissButton;
    private GameObject dismissButtonClone;
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
        thisInformation.isWhite = true;
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
            //check space in front is empty
            if (gridCoordinate.y + 1 < 8) // checking if space is on the board
            {
                checkSpace = new Vector2(gridCoordinate.x, gridCoordinate.y + 1); //checking space directly infront
                checkInfo = chessController.CheckPieceOnSquare(checkSpace); //getting info on piece (if piece is there) on square
                if (checkInfo == null) //if piece doesn't exist
                {
                    possibleMoves.Add(checkSpace); //add coordinate for button
                }
            }
            //check if first move
            if (firstMove && gridCoordinate.y + 2 < 8) //if first move can move two spaces
            {
                if (possibleMoves.Count > 0)//if piece can't move in front then it wont be able to move two in front
                {
                    checkSpace = new Vector2(gridCoordinate.x, gridCoordinate.y + 2); //checking space two infront
                    checkInfo = chessController.CheckPieceOnSquare(checkSpace); //getting info on piece (if piece is there) on square
                    if (checkInfo == null) //if piece doesn't exist
                    {
                        possibleMoves.Add(checkSpace); //add coordinate for button
                    }
                }
            }
            //check diagonal left
            if (gridCoordinate.x - 1 >= 0 && gridCoordinate.y + 1 < 8) //check if space is on board
            {
                checkSpace = new Vector2(gridCoordinate.x - 1, gridCoordinate.y + 1); //checking space up and to the left
                checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                if (checkInfo != null && checkInfo.isWhite == !thisInformation.isWhite) //checking if piece exists AND is piece is opposite colour
                {
                    if (!checkInfo.isKing)//checking if piece is not a king
                    {
                        possibleMoves.Add(checkSpace);
                    }
                }
            }
            //check diagonal right
            if (gridCoordinate.x + 1 < 8 && gridCoordinate.y + 1 < 8) //check if space is on board
            {
                checkSpace = new Vector2(gridCoordinate.x + 1, gridCoordinate.y + 1); //checking space up and to the right
                checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                if (checkInfo != null && checkInfo.isWhite == !thisInformation.isWhite) //checking if piece exists AND is piece is opposite colour
                {
                    if (!checkInfo.isKing)//checking if piece is not a king
                    {
                        possibleMoves.Add(checkSpace);
                    }
                }
            }

            GameObject dismissClone = Instantiate(dismissButton, transform.parent);
            dismissClone.GetComponent<Button>().onClick.AddListener(() => ShowMoves());
            dismissButtonClone = dismissClone;

            for (int i = 0; i < possibleMoves.Count; i++)//spawning buttons
            {
                GameObject buttonClone = Instantiate(moveButton, transform.parent);
                RectTransform rect = buttonClone.GetComponent<RectTransform>();
                Vector2 move = new Vector2((possibleMoves[i].x * gridSize) + gridOrigin.x, (possibleMoves[i].y* gridSize) + gridOrigin.y);
                rect.localPosition = move;
                moveButtons.Add(buttonClone);
                Vector2 moveTo = new Vector2(possibleMoves[i].x, possibleMoves[i].y);
                Button button = buttonClone.GetComponent<Button>();
                button.onClick.AddListener(() => MovePiece(moveTo));
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
        if (firstMove) firstMove = false; //if moved on first move, first move is false
        Vector2 movePos = new Vector2((moveCoordinate.x * gridSize) + gridOrigin.x, (moveCoordinate.y * gridSize) + gridOrigin.y); //selects move pos
        rectTransform.localPosition = movePos; //moves piece
        chessController.TakePiece(moveCoordinate); //asks controller to remove any piece landed on
        gridCoordinate = moveCoordinate; //updates grid coordinate
        thisInformation.gridCoordinate = moveCoordinate; //updates piece information grid coordinate
        chessController.EnablePieces();
    }
}