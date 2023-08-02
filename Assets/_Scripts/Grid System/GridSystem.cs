using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: grid system should only be responsible for UI, not the logic of WFC
/// bottom left cell has coordinate (0,0)
public class GridSystem : MonoBehaviour
{
    public int width;
    public int height;
    public float margin = 0.05f;

    public GameObject cellPrefab;

    // 1D list storing cells in ROW-MAJOR order
    private List<Cell> cells_ = new List<Cell>();

    private Vector2 cellDimension_;

    private void Awake()
    {
        // find suitable world width and height for the grid
        var botLeft = Camera.main.ViewportToWorldPoint(Vector2.zero) * (1 - 2 * margin);
        var topRight = Camera.main.ViewportToWorldPoint(Vector2.one) * (1 - 2 * margin);
        XLogger.Log(Category.GridSystem, $"bot left in world coord: {botLeft}");
        XLogger.Log(Category.GridSystem, $"top right in world coord: {topRight}");
        var cellWidth = (topRight - botLeft).x / width;
        var cellHeight = (topRight - botLeft).y / height;
        cellDimension_ = Vector2.one * Mathf.Min(cellWidth, cellHeight);
        XLogger.Log(Category.GridSystem, $"cell dimension : {cellDimension_}");
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

    public List<Tuple<int,int>> CreateDefaultCells()
    {
        var results = new List<Tuple<int, int>>();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // calculate cell's position
                var pos = new Vector2(
                    (-width / 2f + 0.5f + x) * cellDimension_.x,
                    (-height / 2f + 0.5f + y) * cellDimension_.y
                );
                // create the cell
                var cellObject = Instantiate(cellPrefab, pos, Quaternion.identity);
                cellObject.transform.localScale = cellDimension_;
                var cell = cellObject.GetComponent<Cell>();
                cell.Init(x, y);
                cells_.Add(cell);
                results.Add(new Tuple<int, int>(x,y));
            }
        }
        
        return results;
    }

    public Cell GetCell(int _x, int _y)
    {
        if (_x < 0 || _x >= width || _y < 0 || _y >= height)
        {
            XLogger.LogWarning(Category.GridSystem, $"coordinate {_x},{_y} out of bounds");
            return null;
        }

        if (cells_.Count != width * height)
        {
            XLogger.LogWarning((Category.GridSystem, "GridSystem.cells not properly initialized"));
            return null;
        }

        var index = _x + _y * width;
        return cells_[index];
    }

    public Cell GetCell(Tuple<int, int> _cell)
    {
        return GetCell(_cell.Item1, _cell.Item2);
    }
}