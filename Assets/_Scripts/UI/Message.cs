using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Message : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] float defaultDuration = 1f;
    [SerializeField] Animator animator;


    public void Init(string _text, Color? _color = null, float? _duration = null)
    {
        messageText.text = _text;
        if (_color != null)
        {
            messageText.color = _color.GetValueOrDefault();
        }
        StartCoroutine(Stay(_duration));
    }

    IEnumerator Stay(float? _duration = null)
    {
        yield return new WaitForSecondsRealtime(_duration.GetValueOrDefault(defaultDuration));
        animator.SetTrigger("Leave");
    }
}
