using System;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class TweenBase
{
    public float duration = 1f;
    public bool isLooping = false;

    protected float elapsedTime = 0f;
    protected bool isPlaying = false;

    public void Play()
    {
        TweenManager.Instance.RegisterTween(this);
        isPlaying = true;
        elapsedTime = 0f;
    }

    public void Stop()
    {
        isPlaying = false;
        TweenManager.Instance.DeregisterTween(this);
    }

    public void Restart()
    {
        Stop();
        Play();
    }

    public abstract void UpdateTween(float _deltaTime);
}