using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DivideGridLayout : MonoBehaviour
{
    void Awake()
    {
        UpdateCellSize();
    }

    public void UpdateCellSize()
    {
        GetComponent<GridLayoutGroup>().cellSize = Vector2.one * (GetComponent<RectTransform>().sizeDelta.x / 8.0f);
    }
}
