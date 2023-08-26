using TMPro;
using UnityEngine;

public interface IFloatChangable
{
    float Float { get; set; }
}

public class CanvasGroupFloatAdaptor : IFloatChangable
{
    private readonly CanvasGroup group_;

    public CanvasGroupFloatAdaptor(CanvasGroup _group)
    {
        group_ = _group;
    }

    public float Float { 
        get => group_.alpha;
        set => group_.alpha = value;
    }
}

public class FloatTween : TweenBase
{
    private readonly IFloatChangable target_;
    private readonly float startFloat_;
    private readonly float endFloat_;

    public FloatTween(IFloatChangable _target, float _endFloat, float _duration)
    {
        target_ = _target;
        startFloat_ = _target.Float;
        endFloat_ = _endFloat;
        duration = _duration;
    }

    public override void UpdateTween(float _deltaTime)
    {
        if (!isPlaying) return;

        elapsedTime += _deltaTime;
        var t = elapsedTime / duration;

        target_.Float = Mathf.Lerp(startFloat_, endFloat_, t);

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