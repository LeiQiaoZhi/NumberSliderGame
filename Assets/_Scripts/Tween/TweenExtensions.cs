using TMPro;
using UnityEngine;

public static class TweenExtensions
{
    // transform.DoMove(endPosition, duration);
    public static PositionTween TweenMove(this Transform _target, Vector3 _endPosition, float _duration)
    {
        return new PositionTween(_target, _endPosition, _duration);
    }
    
    public static ColorTween TweenColor(this SpriteRenderer _target, Color _endColor, float _duration)
    {
        return new ColorTween(new SpriteRendererAdapter(_target), _endColor, _duration);
    }
    
    public static ColorTween TweenColor(this TextMeshProUGUI _target, Color _endColor, float _duration)
    {
        return new ColorTween(new TextMeshProUGUIAdapter(_target), _endColor, _duration);
    }
    
    

    // You can add more extension methods here for other tween types.
}