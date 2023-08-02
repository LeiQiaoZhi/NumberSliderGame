using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string header;
    public string content;

    private void OnMouseEnter()
    {
        ToolTipManager.instance.ShowToolTip(header,content);
    }

    private void OnMouseExit()
    {
        ToolTipManager.instance.HideToolTip();
    }

    private void OnDisable()
    {
        ToolTipManager.instance.HideToolTip();
    }

    public void OnPointerEnter(PointerEventData _eventData)
    {
        ToolTipManager.instance.ShowToolTip(header,content);
    }

    public void OnPointerExit(PointerEventData _eventData)
    {
        ToolTipManager.instance.HideToolTip();
    }
}
