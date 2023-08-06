using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollowGrid : MonoBehaviour
{
    [FormerlySerializedAs("camera")] public Camera cam;
    public float speed = 0.1f;
    
    private Vector3 target_;

    private void Start()
    {
        PlayerMovement.OnPlayerMove += OnPlayerMove;
    }

    private void OnPlayerMove(NumberCell _targetCell)
    {
        target_ = _targetCell.transform.position;
    }

    private void LateUpdate()
    {
        // move towards target if not already close
        if (Vector2.SqrMagnitude(cam.transform.position - target_) > 0.0001f)
        {
            Vector3 camPos = cam.transform.position;
            Vector2 lerpPos = Vector2.Lerp(camPos, target_, speed * Time.deltaTime);
            camPos = new Vector3(lerpPos.x, lerpPos.y, camPos.z);
            cam.transform.position = camPos;
        }
    }
}