using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class VisibilityMask : MonoBehaviour
{
    public SpriteMask visibilityMask;
    public AreaType areaType;
    public Vector2Int customVisibleAreaDimension;
    public float borderWidth = 0.1f;

    private InfiniteGridSystem gridSystem_;

    private void Awake()
    {
        gridSystem_ = FindObjectOfType<InfiniteGridSystem>();
    }

    public void ChangeMaskSize()
    {
        Vector2 maskSize = GetMaskSize();
        // Add border
        maskSize = (maskSize + Vector2.one * borderWidth * 2) * gridSystem_.GetCellDimension();
        visibilityMask.transform.localScale = new Vector3(maskSize.x, maskSize.y, 1);
    }

    private Vector2 GetMaskSize()
    {
        switch (areaType)
        {
            case AreaType.GridScreenDimension:
                return gridSystem_.GetSreenAreaDimension();
            case AreaType.GridPatchDimension:
                return gridSystem_.GetPatchDimension();
            case AreaType.CustomDimension:
                return customVisibleAreaDimension;
            default:
                return Vector2.zero;
        }
    }
}

public enum AreaType
{
    GridScreenDimension,
    GridPatchDimension,
    CustomDimension
}