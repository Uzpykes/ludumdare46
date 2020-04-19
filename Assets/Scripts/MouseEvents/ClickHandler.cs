using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{

    public static GameObject clickReceiver;

    public UnityGameObjectEvent OnClickEvent;

    public void OnEnable()
    {
        if (OnClickEvent == null)
            OnClickEvent = new UnityGameObjectEvent();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        clickReceiver = this.gameObject;
        OnClickEvent?.Invoke(clickReceiver);
        clickReceiver = null;
    }
}

public class UnityGameObjectEvent : UnityEvent<GameObject>
{

}
