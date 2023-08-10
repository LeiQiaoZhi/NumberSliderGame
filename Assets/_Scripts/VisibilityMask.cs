using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityMask : MonoBehaviour
{
    public SpriteMask visibilityMask;
    public bool useGridSystemVisibleDimension = true;
    public Vector2Int customVisibleAreaDimension;
    public float borderWidth = 0.1f;

    private InfiniteGridSystem gridSystem_;

    private void Awake()
    {
        gridSystem_ = FindObjectOfType<InfiniteGridSystem>();
    }

    public void ChangeMaskSize()
    {
        Vector2 maskSize =
            (useGridSystemVisibleDimension ? gridSystem_.visibleAreaDimension : customVisibleAreaDimension);
        // Add border
        maskSize = (maskSize + Vector2.one * borderWidth * 2) * gridSystem_.GetCellDimension();
        visibilityMask.transform.localScale = new Vector3(maskSize.x, maskSize.y, 1);
    }
}