using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedUpdateCursorPos : MonoBehaviour
{
    private Camera cam_;

    private void Awake()
    {
        cam_ = Camera.main;
    }

    private void FixedUpdate()
    {
        var mousePos = cam_.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }
}
