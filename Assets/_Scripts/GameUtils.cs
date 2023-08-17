using UnityEngine;

public class GameUtils
{
    public static int ManhattenDistance(Vector2Int _a, Vector2Int _b = default)
    {
        return Mathf.Abs(_a.x - _b.x) + Mathf.Abs(_a.y - _b.y);
    }

    public static bool GuaranteeRandom(int _guarantee, ref int _missCounter)
    {
        if (_missCounter >= _guarantee - 1)
        {
            _missCounter = 0;
            return true;
        }

        var probability = 1 / (_guarantee - _missCounter);
        var result = Random.Range(0.0f, 1.0f) < probability;
        if (result)
            _missCounter = 0;
        else
            _missCounter += 1;
        return result;
    }
    
    public static Vector2 CalculateCellDimension(Camera _cam, float _margin, Vector2Int _dimension)
    {
        // find suitable world cell width and height for the grid
        Vector3 botLeft = _cam.ViewportToWorldPoint(Vector2.zero) * (1 - 2 * _margin);
        Vector3 topRight = _cam.ViewportToWorldPoint(Vector2.one) * (1 - 2 * _margin);
        var cellWidth = (topRight - botLeft).x / _dimension.x;
        var cellHeight = (topRight - botLeft).y / _dimension.y;
        return Vector2.one * Mathf.Min(cellWidth, cellHeight);
    }

}