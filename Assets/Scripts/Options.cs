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

    private string capturePiece = "Queen";

    void Start ()
    {
        chessController = FindObjectOfType<ChessController>();
        beginButton.onClick.AddListener(() => BeginGame());

        pointLimitButtons[0].onClick.AddListener(() => ChangePointLimit(-1));
        pointLimitButtons[1].onClick.AddListener(() => ChangePointLimit(1));
        turnLimitButtons[0].onClick.AddListener(() => ChangeTurnLimit(-1));
        turnLimitButtons[1].onClick.AddListener(() => ChangeTurnLimit(1));

        pointLimitText.text = infinity;
        turnLimitText.text = infinity;
        UpdateRuleText();
    }

    void BeginGame()
    {
        FindObjectOfType<ChessBoardSetUp>().SetUpGame();
        gameObject.SetActive(false);
    }

    void ChangePointLimit(int point)
    {
        pointLimit = Mathf.Clamp(pointLimit + point,0,int.MaxValue);
        pointLimitText.text = pointLimit == 0 ? "\u221E" : pointLimit.ToString();
        chessController.ChangePointAmount(pointLimit);
        UpdateRuleText();
    }

    void ChangeTurnLimit(int turn)
    {
        turnLimit = Mathf.Clamp(turnLimit + turn,0,int.MaxValue);
        turnLimitText.text = turnLimit <= 0? "\u221E":turnLimit.ToString();
        chessController.ChangeTurnAmount(turnLimit);
        UpdateRuleText();
    }

    public ToggleGroup toggleGroup;
    public GameObject[] parentPanels;

    public void ToggleWinCondition()
    {
        // parentPanels[0].SetActive(toggleGroup.togg)
    }

    void UpdateRuleText()
    {
        string rule = "";
        if (goalBools[0])
        {
            rule += pointLimit > 0 ? string.Format("First to {0} {1}", pointLimit,pointLimit==1?"point":"points") : "Highest points";
            rule += turnLimit > 0 ? string.Format(", in {0} {1}.", turnLimit,turnLimit==1?"turn":"turns") : ".";
        }
        if (goalBools[1])
        {
            rule += string.Format("First to capture {0}.", capturePiece);
        }

        ruleText.text = rule;
    }
}
