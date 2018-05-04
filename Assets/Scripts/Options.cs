using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    private ChessController chessController;
    private List<bool> goalBools = new List<bool>();
    public Button beginButton;

    private int pointLimit;
    private int turnLimit;

    public Button[] pointLimitButtons;
    public Button[] turnLimitButtons;
    public Text pointLimitText;
    public Text turnLimitText;

    private string infinity = "\u221E";

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
    }

    void ChangeTurnLimit(int turn)
    {
        turnLimit = Mathf.Clamp(turnLimit + turn,0,int.MaxValue);
        turnLimitText.text = turnLimit <= 0? "\u221E":turnLimit.ToString();
        chessController.ChangeTurnAmount(turnLimit);
    }

    public ToggleGroup toggleGroup;
    public GameObject[] parentPanels;

    public void ToggleWinCondition()
    {
        // parentPanels[0].SetActive(toggleGroup.togg)
    }
}
