using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityMask : MonoBehaviour
{
    public SpriteMask visibilityMask;

    private InfiniteGridSystem gridSystem_;
    private void Awake()
    {
        gridSystem_ = FindObjectOfType<InfiniteGridSystem>();
    }

    public void ChangeMaskSize()
    {
        Vector2 maskSize = gridSystem_.visibleAreaDimension * gridSystem_.GetCellDimension();
        visibilityMask.transform.localScale = new Vector3(maskSize.x, maskSize.y, 1);
    }
}
