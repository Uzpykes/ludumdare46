using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject currentlyDraggedObject;
    public Graphic[] RaycastHandlers;
    Vector3 startPosition;
    private GameObject instantiated;
    private HoverHandler hoverHandler;

    void OnEnable()
    {
        hoverHandler = GetComponent<HoverHandler>();
        RaycastHandlers = GetComponentsInChildren<Graphic>();
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        if (hoverHandler != null)
            hoverHandler.enabled = false;

        currentlyDraggedObject = gameObject;
        startPosition = transform.position;
        foreach(var graphic in RaycastHandlers)
        {
            graphic.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        transform.localPosition += (Vector3)pointerEventData.delta;
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        if (hoverHandler != null)
            hoverHandler.enabled = true;
        currentlyDraggedObject = null;
        transform.position = startPosition;
        foreach(var graphic in RaycastHandlers)
        {
            graphic.raycastTarget = true;
        }
    }
}
