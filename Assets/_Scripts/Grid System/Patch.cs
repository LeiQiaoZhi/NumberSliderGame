using System;
using System.Collections.Generic;
using UnityEngine;

/// bottom left cell has coordinate (0,0)
public class Patch : MonoBehaviour
{
    private int width_;
    private int height_;

    private InfiniteGridSystem infGridSystem_;

    private Vector2 cellDimension_;

    // 1D list storing cells in ROW-MAJOR order
    private List<Cell> cells_ = new List<Cell>();

    public void Init(int _width, int _height, Vector2 _cellDimension, InfiniteGridSystem _infGridSystem)
    {
        width_ = _width;
        height_ = _height;
        cellDimension_ = _cellDimension;
        infGridSystem_ = _infGridSystem;
    }

    public List<Cell> CreateCells()
    {
        for (int y = 0; y < height_; y++)
        {
            for (int x = 0; x < width_; x++)
            {
                // calculate cell's position
                var pos = new Vector2(
                    (-width_ / 2f + 0.5f + x) * cellDimension_.x,
                    (-height_ / 2f + 0.5f + y) * cellDimension_.y
                );
                // create the cell
                GameObject cellObject = Instantiate(infGridSystem_.cellPrefab, transform.position + (Vector3)pos,
                    Quaternion.identity);
                cellObject.transform.SetParent(transform);
                cellObject.transform.localScale = new Vector3(cellDimension_.x, cellDimension_.y, 1);
                var cell = cellObject.GetComponent<Cell>();
                cell.Init(x, y);
                cells_.Add(cell);
            }
        }
        return cells_;
    }

    public Cell GetCell(int _x, int _y)
    {
        if (_x < 0 || _x >= width_ || _y < 0 || _y >= height_)
        {
            XLogger.LogWarning(Category.GridSystem, $"coordinate {_x},{_y} out of bounds");
            return null;
        }

        if (cells_.Count != width_ * height_)
        {
            XLogger.LogWarning((Category.GridSystem, "GridSystem.cells not properly initialized"));
            return null;
        }

        var index = _x + _y * width_;
        return cells_[index];
    }

    public Cell GetCell(Tuple<int, int> _cell)
    {
        return GetCell(_cell.Item1, _cell.Item2);
    }

    public void ClearAllCells()
    {
        List<GameObject> objects = new List<GameObject>();
        foreach (var cell in cells_)
        {
            objects.Add(cell.gameObject);
        }

        cells_ = new List<Cell>();
        foreach (var obj in objects)
        {
            Destroy(obj);
        }

        objects.Clear();
    }

    private void OnDrawGizmosSelected()
    {
        // draw a wired square around the patch
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(width_ * cellDimension_.x, height_ * cellDimension_.y, 0));
    }
}