using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollowGrid : MonoBehaviour
{
    [FormerlySerializedAs("camera")] public Camera cam;
    private PlayerMovement playerMovement_;
    private Vector3 target_;

    private void Start()
    {
        playerMovement_ = FindObjectOfType<PlayerMovement>();
    }

    public void OnPlayerMove()
    {
        target_ = playerMovement_.GetPlayerPositionWorld();
    }

    private void LateUpdate()
    {
        // move towards target if not already close
        if (Vector2.SqrMagnitude(cam.transform.position - target_) > 0.01f)
        {
            Vector3 camPos = cam.transform.position;
            Vector2 lerpPos = Vector2.Lerp(camPos, target_, 0.1f);
            camPos = new Vector3(lerpPos.x, lerpPos.y, camPos.z);
            cam.transform.position = camPos;
        }
    }
}