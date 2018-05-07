using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    private ChessController chessController;
    private List<bool> goalBools = new List<bool>() { true, false };
    public Button beginButton;

    private int pointLimit;
    private int turnLimit;

    public Button[] pointLimitButtons;
    public Button[] turnLimitButtons;
    public Text pointLimitText;
    public Text turnLimitText;

    private string infinity = "\u221E";

    public Text ruleText;

    private int currentPiece;
    public Button[] choosePieceButtons;
    public Text choosePieceText;
    public Image choosePieceIcon;
    public Sprite[] pieceSprites;
    private List<string> capturePiece = new List<string>()
    {
        "Queen",
        "Bishop",
        "Knight",
        "Rook",
        "Pawn"
    };

    public ToggleGroup toggleGroup;
    public Toggle[] winConditionToggles;
    public GameObject[] parentPanels;

    public Text inGameRules;

    void Start ()
    {
        chessController = FindObjectOfType<ChessController>();
        beginButton.onClick.AddListener(() => BeginGame());

        pointLimitButtons[0].onClick.AddListener(() => ChangePointLimit(-1));
        pointLimitButtons[1].onClick.AddListener(() => ChangePointLimit(1));
        turnLimitButtons[0].onClick.AddListener(() => ChangeTurnLimit(-1));
        turnLimitButtons[1].onClick.AddListener(() => ChangeTurnLimit(1));

        choosePieceButtons[0].onClick.AddListener(() => ChangeCapturePiece(1));
        choosePieceButtons[1].onClick.AddListener(() => ChangeCapturePiece(-1));

        pointLimitText.text = infinity;
        turnLimitText.text = infinity;
        UpdateRuleText();
        CheckInteractive();
    }

    void CheckInteractive()
    {
        beginButton.interactable = !(goalBools[0] && pointLimit + turnLimit == 0);
    }

    void BeginGame()
    {
        FindObjectOfType<ChessBoardSetUp>().SetUpGame();
        gameObject.SetActive(false);
    }

    void ChangeCapturePiece(int piece)
    {
        currentPiece = (currentPiece + piece >= 0) ? ((currentPiece + piece) % capturePiece.Count) : (capturePiece.Count - 1);
        choosePieceText.text = capturePiece[currentPiece];
        choosePieceIcon.sprite = pieceSprites[currentPiece];
        UpdateRuleText();
    }

    void ChangePointLimit(int point)
    {
        pointLimit = Mathf.Clamp(pointLimit + point,0,int.MaxValue);
        pointLimitText.text = pointLimit == 0 ? "\u221E" : pointLimit.ToString();
        chessController.ChangePointAmount(pointLimit);
        UpdateRuleText();
        CheckInteractive();
    }

    void ChangeTurnLimit(int turn)
    {
        turnLimit = Mathf.Clamp(turnLimit + (turn*2),0,int.MaxValue);
        turnLimitText.text = turnLimit <= 0? "\u221E":turnLimit.ToString();
        chessController.ChangeTurnAmount(turnLimit);
        UpdateRuleText();
        CheckInteractive();
    }
    
    public void ToggleWinCondition()
    {
        for (int i = 0; i < winConditionToggles.Length; i++)
        {
            goalBools[i] = winConditionToggles[i].isOn;
            parentPanels[i].SetActive(winConditionToggles[i].isOn);
        }

        UpdateRuleText();
    }

    void UpdateRuleText()
    {
        string rule = "";
        if (goalBools[0])
        {
            rule += pointLimit > 0 ? string.Format("First to {0} {1}", pointLimit,pointLimit==1?"point":"points") : "Highest points";
            rule += turnLimit > 0 ? string.Format(", in {0} {1}.", turnLimit,turnLimit==1?"turn":"turns") : ".";
            if (turnLimit + pointLimit == 0)
                rule = "Highest points in unlimited turns, select either a point limit or turn limit to play.";
        }
        if (goalBools[1])
        {
            rule += string.Format("First to capture the opponent's {0}.", capturePiece[currentPiece]);
        }

        inGameRules.text = string.Format("Rules: {0}", rule);
        ruleText.text = rule;
    }
}
