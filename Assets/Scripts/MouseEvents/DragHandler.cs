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
    private Canvas sceneCanvas;
    private RectTransform canvasRectTransform;

    void OnEnable()
    {
        hoverHandler = GetComponent<HoverHandler>();
        RaycastHandlers = GetComponentsInChildren<Graphic>();
        sceneCanvas = GetComponentInParent<Canvas>();
        canvasRectTransform = sceneCanvas.GetComponent<RectTransform>();
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
        // Debug.Log(sceneCanvas.scaleFactor);
        // var dx = pointerEventData.delta.x * sceneCanvas.scaleFactor;
        // var dy = pointerEventData.delta.y * sceneCanvas.scaleFactor;
        // transform.localPosition += (Vector3)(new Vector2(dx, dy));

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, pointerEventData.position, Camera.main, out pos);
 
        transform.position = canvasRectTransform.TransformPoint(pos);
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
