using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayout))]
public class GridSpaceController : MonoBehaviour
{
    private GridLayoutGroup gridLayout;

    private RectTransform rectTransform;
    private void Awake()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
  
    }

    private void Update()
    {
        float ModeOfWithSpace = rectTransform.rect.width - (3 * gridLayout.cellSize.x);

        gridLayout.spacing = new Vector2(Mathf.Abs(ModeOfWithSpace/3.0f), gridLayout.spacing.y);
    }
}
