using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowFixedTarget : MonoBehaviour
{
    public Transform self;
    public Transform player;
    public Transform zero;
    
    public float speed = 0.1f;
    private Transform target_;

    private void OnEnable()
    {
        NumberGridGenerator.OnGenerationStart += SetTarget;
    }

    private void OnDisable()
    {
        NumberGridGenerator.OnGenerationStart -= SetTarget;
    }

    private void SetTarget(World _world, InfiniteGridSystem _gridsystem)
    {
        target_ = (_world.cameraFollow) ? player : zero;
    }

    private void LateUpdate()
    {
        // move towards target if not already close
        if (Vector2.SqrMagnitude(self.transform.position - target_.position) > 0.0001f)
        {
            Vector3 camPos = self.position;
            Vector2 lerpPos = Vector2.Lerp(camPos, target_.position, speed * Time.deltaTime);
            camPos = new Vector3(lerpPos.x, lerpPos.y, camPos.z);
            self.transform.position = camPos;
        }
    }
}