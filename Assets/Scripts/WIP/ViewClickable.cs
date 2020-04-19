using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

/// <summary>
/// Specifically, these are the monobehavior accessing point to a world object that is targetable.
/// Use this to get access to a world object through the mouse, or to find a component on a targetable
/// world object.
/// </summary>
public class ViewClickable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public event Action Clicked = delegate { };
    // note, the below 2 events are only called if this object is targetable
    public event Action MouseEntered = delegate { };
    public event Action MouseExited = delegate { };

    bool _isHovering = false;

    public bool IsCurrentlyClickable { get; private set; } = false;

    public void SetClickable(bool isTargetable)
    {
        IsCurrentlyClickable = isTargetable;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsCurrentlyClickable)
        {
            _isHovering = true;
            MouseEntered.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsCurrentlyClickable)
        {
            _isHovering = false;
            MouseExited.Invoke();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // if we're able to click it, and we're hovering over it already...
        if (IsCurrentlyClickable && _isHovering)
        {
            Clicked?.Invoke();
        }
    }
}
