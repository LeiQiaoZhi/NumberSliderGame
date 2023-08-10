using UnityEngine;

public class PositionTween : TweenBase
{
    private Transform target_;
    private Vector3 startPosition_;
    private Vector3 endPosition_;

    public PositionTween(Transform _target, Vector3 _endPosition, float _duration)
    {
        target_ = _target;
        startPosition_ = _target.position;
        endPosition_ = _endPosition;
        duration = _duration;
    }

    public override void UpdateTween(float _deltaTime)
    {
        if (!isPlaying) return;

        elapsedTime += _deltaTime;
        var t = elapsedTime / duration;

        // TODO: other easing functions
        target_.position = Vector3.Lerp(startPosition_, endPosition_, t);

        if (t >= 1)
        {
            if (isLooping)
            {
                Restart();
            }
            else
            {
                Stop();
            }
        }
    }
}
