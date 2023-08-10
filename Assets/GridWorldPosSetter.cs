using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridWorldPosSetter : MonoBehaviour
{
    public Vector2 gridPos; // of center, relative to player position
    public Vector2 size; // relative to cell size
    private InfiniteGridSystem gridSystem_;

    void Start()
    {
        gridSystem_ = FindObjectOfType<InfiniteGridSystem>();
        transform.localScale = new Vector3(gridSystem_.GetCellDimension().x * size.x,
            gridSystem_.GetCellDimension().y * size.y, 1);
        transform.position =
            new Vector3(gridSystem_.GetCellDimension().x * gridPos.x, gridSystem_.GetCellDimension().y * gridPos.y, 1);
    }
}