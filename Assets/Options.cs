using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Button[] goalButtons;
    public GameObject[] goalPanels;
    private ChessController chessController;

	void Start ()
    {
        chessController = FindObjectOfType<ChessController>();
        goalButtons[0].onClick.AddListener(() => SwapGoalType(0));
        goalButtons[1].onClick.AddListener(() => SwapGoalType(1));
        goalButtons[2].onClick.AddListener(() => SwapGoalType(2));
    }
	
	// Update is called once per frame
	void SwapGoalType (int type)
    {
        for (int i = 0; i < 3; i++)
        {
            if(i != type)
            {
                goalButtons[i].interactable = true;
                goalPanels[i].SetActive(false);
            }
            else
            {
                goalButtons[i].interactable = false;
                goalPanels[i].SetActive(true);
            }
        }

        switch (type)
        {
            case 0:

        }
	}
}
