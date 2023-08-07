using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Sprite cursorSprite;
    public bool useUI = true;
    public RectTransform cursorUI;

    SpriteRenderer cursorSpriteRenderer_;
    private Camera cam_;


    // Start is called before the first frame update
    void Start()
    {
        cam_ = Camera.main;
        Cursor.visible = false;
        if (!useUI)
        {
            cursorSpriteRenderer_ = GetComponentInChildren<SpriteRenderer>();
            cursorSpriteRenderer_.sprite = cursorSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (useUI)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(cursorUI.parent as RectTransform,
                Input.mousePosition,
                null, out var mousePos);
            cursorUI.anchoredPosition = mousePos;
        }
        else
        {
            Vector2 mousePos = cam_.ScreenToWorldPoint(Input.mousePosition);
            cursorSpriteRenderer_.transform.position = mousePos;
        }
    }
}