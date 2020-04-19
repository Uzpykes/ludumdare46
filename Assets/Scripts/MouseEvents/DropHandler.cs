using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
public class DropHandler : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image containerImage;
    public Color normalColor;
    public Color disabledColor;
    public Color highlightedColor;
    public UnityEvent OnDropEvent;

    public void OnEnable()
    {
        containerImage.color = normalColor;
    }

    public void OnDisable()
    {
        containerImage.color = disabledColor;
    }

    public void OnDrop(PointerEventData pointerEventData)
    {
        //Only run if drag handler is draggin something
        if (DragHandler.currentlyDraggedObject != null)
        {
            OnDropEvent.Invoke();
            containerImage.color = normalColor;
            DragHandler.currentlyDraggedObject = null;
        }

    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {   
        containerImage.color = highlightedColor;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        containerImage.color = normalColor;
    }

}
