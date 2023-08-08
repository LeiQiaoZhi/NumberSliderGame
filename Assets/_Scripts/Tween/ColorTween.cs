using TMPro;
using UnityEngine;

public interface IColorable
{
    Color Color { get; set; }
    Object  GetTargetObject();
}

public class SpriteRendererAdapter : IColorable
{
    private readonly SpriteRenderer renderer_;

    public SpriteRendererAdapter(SpriteRenderer _renderer)
    {
        renderer_ = _renderer;
    }

    public Color Color
    {
        get => renderer_.color;
        set => renderer_.color = value;
    }
    public Object GetTargetObject()
    {
        return renderer_;
    }
}

public class TextMeshProUGUIAdapter : IColorable
{
    private readonly TextMeshProUGUI text_;

    public TextMeshProUGUIAdapter(TextMeshProUGUI _text)
    {
        text_ = _text;
    }
    public Color Color
    {
        get => text_.color;
        set => text_.color = value;
    }
    public Object GetTargetObject()
    {
        return text_;
    }
}

public class ColorTween : TweenBase
{
    private readonly IColorable target_;
    private readonly Color startColor_;
    private readonly Color endColor_;

    public ColorTween(IColorable _target, Color _endColor, float _duration)
    {
        target_ = _target;
        startColor_ = _target.Color;
        endColor_ = _endColor;
        duration = _duration;
        targetObject = _target.GetTargetObject();
    }

    public override void UpdateTween(float _deltaTime)
    {
        if (!isPlaying) return;

        elapsedTime += _deltaTime;
        var t = elapsedTime / duration;

        target_.Color = Color.Lerp(startColor_, endColor_, t);

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