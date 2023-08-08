using UnityEngine;

public class ColorTween : TweenBase
{
    private SpriteRenderer target_;
    private Color startColor_;
    private Color endColor_;
    
    public ColorTween(SpriteRenderer _target, Color _endColor, float _duration)
    {
        target_ = _target;
        startColor_ = _target.color;
        endColor_ = _endColor;
        duration = _duration;
        targetObject = _target;
    }

    public override void UpdateTween(float _deltaTime)
    {
        if (!isPlaying) return;

        elapsedTime += _deltaTime;
        var t = elapsedTime / duration;
        
        target_.color = Color.Lerp(startColor_, endColor_, t);
        
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