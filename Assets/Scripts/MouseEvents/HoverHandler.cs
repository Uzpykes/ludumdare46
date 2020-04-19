using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Vector3 MoveOnHover;
    public HoverReactionType Type;

    private Vector3 startPosition;
    private Vector3 startScale;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        startPosition = transform.position;
        startScale = transform.localScale;
        if (Type == HoverReactionType.Shift)
        {
            transform.position += MoveOnHover;
        }
        else if (Type == HoverReactionType.Scale)
        {
            transform.localScale += MoveOnHover;
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (Type == HoverReactionType.Shift)
        {
            transform.position = startPosition;
        }
        else if (Type == HoverReactionType.Scale)
        {
            transform.localScale = startScale;
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (Type == HoverReactionType.Shift)
        {
            transform.position = startPosition;
        }
        else if (Type == HoverReactionType.Scale)
        {
            transform.localScale = startScale;
        }
    }

}

public enum HoverReactionType
{
    Shift,
    Scale
}
