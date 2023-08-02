using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float cursorContribution = 0.2f;

    private Camera cam_;

    // Update is called once per frame
    private void Start()
    {
        cam_= Camera.main;
    }

    void Update()
    {
        var mousePos = cam_.ScreenToWorldPoint(Input.mousePosition);
        transform.position = player.position + (mousePos-player.position) * cursorContribution;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}