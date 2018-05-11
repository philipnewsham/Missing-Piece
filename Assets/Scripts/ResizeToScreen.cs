using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeToScreen : MonoBehaviour
{
    public float paddingPercent;

	void Awake()
    {
        float paddingHeight = (Screen.height / 100.0f) * (paddingPercent * 2);
        GetComponent<RectTransform>().sizeDelta = Vector2.one * (Screen.height - paddingHeight);
        GetComponent<DivideGridLayout>().UpdateCellSize();
    }
}
