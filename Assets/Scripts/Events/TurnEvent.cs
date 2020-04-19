using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurnEvent
{
    public string EventName;
    public Sprite EventSprite;
    public string EventDescription;
    [System.NonSerialized]
    public int TurnDrawn = 0;

    public List<DurationEffect> ResourceEffects;
    public List<DrawEffect> DrawEffects;

    public TurnEvent(string eventName, Sprite eventSprite, string eventDescription, List<DurationEffect> resourceEffects, List<DrawEffect> drawEffects)
    {
        EventName = eventName;
        EventSprite = eventSprite;
        EventDescription = eventDescription;
        ResourceEffects = resourceEffects;
        DrawEffects = drawEffects;
    }
}


