using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowGridTarget : MonoBehaviour
{
    public Transform self;
    public float speed = 0.1f;

    private Vector3 target_;

    private void OnEnable()
    {
        PlayerMovement.OnPlayerMove += OnPlayerMove;
    }
    
    private void OnDisable()
    {
        PlayerMovement.OnPlayerMove -= OnPlayerMove;
    }

    private void OnPlayerMove(PlayerMovement.MergeResult _mergeResult)
    {
        if (_mergeResult.targetTransform == null) return;
        target_ = _mergeResult.targetTransform.position;
    }

    private void LateUpdate()
    {
        // move towards target if not already close
        if (Vector2.SqrMagnitude(self.transform.position - target_) > 0.0001f)
        {
            Vector3 camPos = self.position;
            Vector2 lerpPos = Vector2.Lerp(camPos, target_, speed * Time.deltaTime);
            camPos = new Vector3(lerpPos.x, lerpPos.y, camPos.z);
            self.transform.position = camPos;
        }
        else
            self.transform.position = new Vector3(target_.x, target_.y, self.transform.position.z);
    }
}