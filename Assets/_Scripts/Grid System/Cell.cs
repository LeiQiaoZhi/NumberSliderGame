using UnityEngine;

public class Cell : MonoBehaviour
{
    public int x;
    public int y;
    public SpriteRenderer spriteRenderer;

    public void Init(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public override string ToString()
    {
        return $"cell ({x},{y})";
    }

    public void SetSprite(Sprite _itemImage)
    {
        spriteRenderer.sprite = _itemImage;
        spriteRenderer.color = Color.white;
    }
}