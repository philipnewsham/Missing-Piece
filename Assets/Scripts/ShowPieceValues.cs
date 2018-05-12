using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPieceValues : MonoBehaviour
{
    private List<Text> scoreTexts = new List<Text>();

	public void ShowValues(int[] scores)
    {
        scoreTexts.Clear();
        scoreTexts.AddRange(GetComponentsInChildren<Text>());
        for (int i = 0; i < scoreTexts.Count; i++)
            scoreTexts[i].text = scores[i].ToString();
    }
}
