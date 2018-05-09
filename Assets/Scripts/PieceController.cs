using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceController : MonoBehaviour
{
    public PieceTitle.Piece piece;
    public Sprite[] pieceImages;

    public Vector2 gridCoordinate;
    private RectTransform rectTransform;
    private ChessController chessController;
    private bool showMoves = false;
    private List<GameObject> moveButtons = new List<GameObject>();
    public GameObject moveButton;
    private float gridSize;
    private Vector2 gridOrigin;
    public GameObject dismissButton;
    private GameObject dismissButtonClone;
    public bool isWhite;
    public bool firstMove = true;
    public int score;
    public bool isKing;

    private AudioSource audioSource;

    private ShowMoves showMoveScript;

    void Start()
    {
        chessController = FindObjectOfType<ChessController>();
        showMoveScript = FindObjectOfType<ShowMoves>();
        gridSize = chessController.gridSize;
        gridOrigin = chessController.gridOrigin;
        rectTransform = GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();
        GetComponent<Button>().onClick.AddListener(() => { ShowMoves(); audioSource.Play(); });
    }

    void ShowMoves()
    {
        showMoves = !showMoves;

        if (showMoves)
        {
            List<Vector2> possibleMoves = showMoveScript.ShowPossibleMoves(gridCoordinate, isWhite, piece, firstMove);

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
        else
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
        firstMove = false;

        if (CheckPawnUpgrade())
            UpgradeToQueen();

        chessController.EnablePieces();
        audioSource.Play();
    }

    bool CheckPawnUpgrade()
    {
        if (piece != PieceTitle.Piece.PAWN)
            return false;

        if (isWhite)
            return (gridCoordinate.y == 7);
        else
            return (gridCoordinate.y == 0);
    }

    void UpgradeToQueen()
    {
        piece = PieceTitle.Piece.QUEEN;
        GetComponent<Image>().sprite = FindObjectOfType<ChessBoardSetUp>().pieceSprites[(int)piece + (System.Enum.GetValues(typeof(PieceTitle.Piece)).Length * (isWhite ? 0 : 1))];
    }
}