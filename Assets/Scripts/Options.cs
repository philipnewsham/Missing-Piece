using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    private ChessController chessController;
    private ChessBoardSetUp chessSetUp;
    private List<bool> goalBools = new List<bool>() { true, false };
    public Button beginButton;
    public Button resetButton;

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
        chessSetUp = FindObjectOfType<ChessBoardSetUp>();
        beginButton.onClick.AddListener(() => BeginGame());
        resetButton.onClick.AddListener(() => ResetOptions());

        choosePieceButtons[0].onClick.AddListener(() => ChangeCapturePiece(1));
        choosePieceButtons[1].onClick.AddListener(() => ChangeCapturePiece(-1));

        pointLimitText.text = infinity;
        turnLimitText.text = infinity;

        ToggleWinCondition();
    }
    
    void BeginGame()
    {
        FindObjectOfType<ChessBoardSetUp>().SetUpGame();
        gameObject.SetActive(false);
    }

    void ChangeCapturePiece(int piece)
    {
        currentPiece = (currentPiece + piece >= 0) ? ((currentPiece + piece) % capturePiece.Count) : (capturePiece.Count - 1);
        chessController.capturePiece = (PieceTitle.Piece)currentPiece;
        choosePieceText.text = capturePiece[currentPiece];
        choosePieceIcon.sprite = pieceSprites[currentPiece];
        UpdateRuleText();
    }

    public Slider pointSlider;
    public Slider turnSlider;

    public void ChangePointLimit()
    {
        pointLimit = Mathf.Clamp((int)pointSlider.value,0,int.MaxValue);
        pointLimitText.text = pointLimit == 0 ? "\u221E" : pointLimit.ToString();
        chessController.ChangePointAmount(pointLimit);
        UpdateRuleText();
        CheckInteractive();
    }

    public void ChangeTurnLimit()
    {
        turnLimit = Mathf.Clamp(((int)turnSlider.value * 2),0,int.MaxValue);
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
            chessController.goalCapture = goalBools[1];
        }
        CheckInteractive();
        UpdateRuleText();
    }

    void UpdateRuleText()
    {
        string rule = "";
        if (goalBools[0])
        {
            rule += pointLimit > 0 ? string.Format("First to {0} {1}", pointLimit,pointLimit==1?"point":"points") : "Highest points";
            rule += turnLimit > 0 ? string.Format(", or the highest score in {0} {1}.", turnLimit,turnLimit==1?"turn":"turns") : ".";
            if (turnLimit == 0)
            {
                if (pointLimit == 0)
                    rule = "Highest points in unlimited turns can't be won! Select either a point limit or turn limit to play.";
                else if (!chessSetUp.PointLimitPossible(pointLimit))
                    rule = "Based on the current pieces values, the current point aim is impossible!";
            }
        }
        if (goalBools[1])
            rule += string.Format("First to capture the opponent's {0}.", capturePiece[currentPiece]);

        inGameRules.text = string.Format("Rules: {0}", rule);
        ruleText.text = rule;
    }

    void CheckInteractive()
    {
        if (goalBools[0])
        {
            if (turnLimit == 0)
                beginButton.interactable = (pointLimit == 0) ? false : chessSetUp.PointLimitPossible(pointLimit);
            else
                beginButton.interactable = true;
        }

        if (goalBools[1])
            beginButton.interactable = true;
    }

    public void UpdateRules()
    {
        UpdateRuleText();
        CheckInteractive();
    }

    void ResetOptions()
    {
        pointSlider.value = 0;
        turnSlider.value = 0;
        ChangeCapturePiece(-currentPiece);
        winConditionToggles[0].isOn = true;
        chessSetUp.ResetScores();
        UpdateRules();
    }
}
