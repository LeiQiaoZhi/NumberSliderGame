using TMPro;
using UnityEngine;

public class NumberTween : TweenBase
{
    private TextMeshProUGUI target_;
    private int startNumber_;
    private int endNumber_;
    private float minDeltaTime_;
    private float debtTime_;

    public NumberTween(TextMeshProUGUI _target, int _endNumber, float _duration, int _maxSteps)
    {
        target_ = _target;
        startNumber_ = int.Parse(_target.text);
        endNumber_ = _endNumber;
        duration = _duration;
        minDeltaTime_ = duration / _maxSteps;
    }

    public override void UpdateTween(float _deltaTime)
    {
        if (!isPlaying || target_ == null) return;
        elapsedTime += _deltaTime;
        if (debtTime_ < minDeltaTime_)
        {
            debtTime_ += _deltaTime;
            return;
        }

        debtTime_ = 0;

        var t = elapsedTime / duration;

        // TODO: other easing functions
        if (Mathf.Abs(startNumber_ - endNumber_) == 1)
            target_.text = endNumber_.ToString();
        else
            target_.text = Mathf.RoundToInt(Mathf.Lerp(startNumber_, endNumber_, t)).ToString();

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