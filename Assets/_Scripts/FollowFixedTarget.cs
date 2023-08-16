using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowFixedTarget : MonoBehaviour
{
    public Transform self;
    public Transform target;
    public float speed = 0.1f;
    private void LateUpdate()
    {
        // move towards target if not already close
        if (Vector2.SqrMagnitude(self.transform.position - target.position) > 0.0001f)
        {
            Vector3 camPos = self.position;
            Vector2 lerpPos = Vector2.Lerp(camPos, target.position, speed * Time.deltaTime);
            camPos = new Vector3(lerpPos.x, lerpPos.y, camPos.z);
            self.transform.position = camPos;
        }
    }
}