using System.Collections.Generic;
using UnityEngine;

public class TweenManager : MonoBehaviour
{
    private static TweenManager _instance;

    public static TweenManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("TweenManager").AddComponent<TweenManager>();
            }

            return _instance;
        }
    }

    private List<TweenBase> activeTweens_ = new();

    private void Update()
    {
        for (int i = activeTweens_.Count - 1; i >= 0; i--)
        {
            activeTweens_[i]?.UpdateTween(Time.deltaTime);
        }
    }

    public void RegisterTween(TweenBase _tween)
    {
        // TweenBase duplicateTween = activeTweens_.Find(_t => _t.targetObject == _tween.targetObject);
        // if (duplicateTween != null)
        // {
        //     // override existing tween
        //     XLogger.Log(Category.Tween, $"Overriding existing tween for {_tween.targetObject.name}");
        //     duplicateTween.Stop();
        // }

        activeTweens_.Add(_tween);
    }

    public void DeregisterTween(TweenBase _tween)
    {
        activeTweens_.Remove(_tween);
    }
}