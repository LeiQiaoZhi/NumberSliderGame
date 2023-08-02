using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RotateHelper  
{
    /// <summary>
    /// Smooth -- faster when angle between is large
    /// <para>- is affected by angular drag</para>
    /// </summary>
    /// <param name="_rb">the RB2D to apply rotation</param>
    /// <param name="_targetPos">rotate towards target</param> 
    /// <param name="_referenceDirection">which direction to align to target, e.g. <c>tranform.right</c></param>
    public static void SmoothRotateTowards(Rigidbody2D _rb, Vector2 _targetPos, Vector2 _referenceDirection  ,float _rotateSpeed)
    {
        var direction = (Vector2)((Vector3)_targetPos - _rb.transform.position).normalized;
        float rotateAmount = Vector3.Cross(direction, _referenceDirection).z;
        _rb.angularVelocity = -rotateAmount * _rotateSpeed;
    }

    /// <summary>
    /// returns angle from 0 to 180 degrees
    /// <code>
    /// AngleBetween(transform.position, target.position, transform.right)
    /// </code>
    /// </summary>
    /// <returns></returns>
    public static float AngleBetween(Vector3 _originPosition, Vector3 _targetPosition, Vector3 _referenceDirection)
    {
        var direction = (Vector2)(_targetPosition - _originPosition).normalized;
        var angle = Mathf.Acos(Vector2.Dot(direction, _referenceDirection.normalized))*Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle += 180;
        }

        return angle;
    }
}