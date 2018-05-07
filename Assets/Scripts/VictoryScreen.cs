using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    public Button replayButton;
    public Button quitButton;

    public GameObject optionScreen;

	void Start ()
    {
        replayButton.onClick.AddListener(() => Replay());
        quitButton.onClick.AddListener(() => Application.Quit());
	}
	
	void Replay ()
    {
        FindObjectOfType<ChessController>().Reset();
        optionScreen.SetActive(true);
        gameObject.SetActive(false);
	}
}
