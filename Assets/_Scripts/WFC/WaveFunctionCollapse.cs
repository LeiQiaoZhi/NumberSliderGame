using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class WaveFunctionCollapse : MonoBehaviour
{
    public List<WfcItem> wfcItems;
    [SerializeField] private float secondsBetweenCollapse = 1f;
    // responsible for UI of the grid
    public GridSystem gridSystem;

    // created from wfcItems
    private List<WfcItem> itemsIncludingRotations_;
    private List<Tuple<int, int>> cells_;

    // Start is called before the first frame update
    void Start()
    {
        // create new items based on rotations
        CreateItemsBaseOnRotation();
        XLogger.Log(Category.WFC,
            $"{itemsIncludingRotations_.Count} items now, {itemsIncludingRotations_.Count - wfcItems.Count} created from rotation");

        cells_ = gridSystem.CreateDefaultCells(itemsIncludingRotations_);
        // StartWFC();
    }

    public void StartWfc()
    {
        StopAllCoroutines();
        gridSystem.ClearAllCells();
        cells_ = gridSystem.CreateDefaultCells(itemsIncludingRotations_);
        StartCoroutine(DoWaveFunctionCollapse());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator DoWaveFunctionCollapse()
    {
        // var heap = new Heap<Cell>(cells, (x, y) => x.GetEntropy() - y.GetEntropy());
        // XLogger.Log(Category.WFC, $"heap created, with count = {heap.Count}");
        bool first = true;
        while (cells_.Count > 0)
        {
            cells_.Sort((_cell1, _cell2) => gridSystem.GetCell(_cell1.Item1, _cell1.Item2).GetEntropy() -
                                         gridSystem.GetCell(_cell2.Item1, _cell2.Item2).GetEntropy());
            // get the cell with the least entropy
            var cell = gridSystem.GetCell(cells_[0].Item1, cells_[0].Item2);
            if (first)
            {
                cell = gridSystem.GetCell(Mathf.RoundToInt(gridSystem.width / 2), Mathf.RoundToInt(gridSystem.height / 2));
                first = false;
                cells_.Remove(new Tuple<int, int>(cell.x,cell.y));
            }
            else
                cells_.RemoveAt(0);
            
            XLogger.Log(Category.WFC, $"cell ({cell.x},{cell.y}) with entropy {cell.GetEntropy()} is popped");
            
            if (cell.GetEntropy() == 0)
                XLogger.LogWarning($"cell ({cell.x},{cell.y}) has zero possible choices");
            else 
                cell.Collapse();

            if (secondsBetweenCollapse > 0)
                yield return new WaitForSeconds(secondsBetweenCollapse);
            else
                yield return null;
        }

        XLogger.LogWarning(Category.WFC, "WAVE FUNCTION COLLAPSED FINISHED");
    }


    void CreateItemsBaseOnRotation()
    {
        itemsIncludingRotations_ = new List<WfcItem>(wfcItems);
        foreach (var itemBase in wfcItems)
        {
            var item = itemBase as WfcItemEdges;
            if (item == null) continue;
            if (!(item.rotate90 || item.rotate180 || item.rotate270)) continue;
            var image90 = RotateSpriteClockwise(item.image);
            var image180 = RotateSpriteClockwise(image90);
            var image270 = RotateSpriteClockwise(image180);
            if (item.rotate90)
            {
                var rotateItem = ScriptableObject.CreateInstance<WfcItemEdges>();
                rotateItem.name = $"{item.name}-90";
                rotateItem.image = image90;
                rotateItem.topEdges = item.leftEdges;
                rotateItem.leftEdges = item.downEdges;
                rotateItem.downEdges = item.rightEdges;
                rotateItem.rightEdges = item.topEdges;
                XLogger.Log(Category.WFC, $"90-degree rotated {item} is created");
                rotateItem.topRule = item.topRule;
                rotateItem.downRule = item.downRule;
                rotateItem.leftRule = item.leftRule;
                rotateItem.rightRule = item.rightRule;
                itemsIncludingRotations_.Add(rotateItem);
            }

            if (item.rotate180)
            {
                var rotateItem = ScriptableObject.CreateInstance<WfcItemEdges>();
                rotateItem.name = $"{item.name}-180";
                rotateItem.image = image180;
                rotateItem.topEdges = item.downEdges;
                rotateItem.leftEdges = item.rightEdges;
                rotateItem.downEdges = item.topEdges;
                rotateItem.rightEdges = item.leftEdges;
                XLogger.Log(Category.WFC, $"180-degree rotated {item} is created");
                rotateItem.topRule = item.topRule;
                rotateItem.downRule = item.downRule;
                rotateItem.leftRule = item.leftRule;
                rotateItem.rightRule = item.rightRule;
                itemsIncludingRotations_.Add(rotateItem);
            }

            if (item.rotate270)
            {
                var rotateItem = ScriptableObject.CreateInstance<WfcItemEdges>();
                rotateItem.name = $"{item.name}-270";
                rotateItem.image = image270;
                rotateItem.topEdges = item.rightEdges;
                rotateItem.leftEdges = item.topEdges;
                rotateItem.downEdges = item.leftEdges;
                rotateItem.rightEdges = item.downEdges;
                XLogger.Log(Category.WFC, $"270-degree rotated {item} is created");
                rotateItem.topRule = item.topRule;
                rotateItem.downRule = item.downRule;
                rotateItem.leftRule = item.leftRule;
                rotateItem.rightRule = item.rightRule;
                itemsIncludingRotations_.Add(rotateItem);
            }
        }
    }

    Sprite RotateSpriteClockwise(Sprite _sprite)
    {
        // Create a new texture to hold the rotated pixels
        Texture2D rotatedTexture = new Texture2D(_sprite.texture.height, _sprite.texture.width);
        rotatedTexture.filterMode = FilterMode.Point;

        // Rotate the pixels by 90 degrees clockwise
        for (int x = 0; x < _sprite.texture.width; x++)
            for (int y = 0; y < _sprite.texture.height; y++)
                rotatedTexture.SetPixel(y, _sprite.texture.width - x - 1, _sprite.texture.GetPixel(x, y));

        // Apply the rotated pixels to the texture and create a new sprite
        rotatedTexture.Apply();
        return Sprite.Create(rotatedTexture, _sprite.rect, new Vector2(0.5f, 0.5f), _sprite.pixelsPerUnit);
    }

    public WFCCell GetCell(int _p0, int _p1)
    {
        throw new NotImplementedException();
    }
}