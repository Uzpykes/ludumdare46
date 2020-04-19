using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventCardBehaviour : MonoBehaviour
{
    [System.NonSerialized]
    public TurnEvent EventProperties;

    public TextMeshProUGUI Title;
    public Image Image;
    public TextMeshProUGUI Description;
    public RectTransform EffectsHolder;

    public HoverHandler hoverHandler;

    private List<UICardEffect> EffectDrawers;

    public void OnEnable()
    {
        hoverHandler = GetComponent<HoverHandler>();
        EffectDrawers = new List<UICardEffect>();
    }

    public void SetEventProperties(TurnEvent ev)
    {
        EventProperties = ev;
        Title.text = EventProperties.EventName;
        Description.text = EventProperties.EventDescription;
        Image.sprite = EventProperties.EventSprite;
    }

    public void AddEffectDrawer(UICardEffect effect)
    {
        EffectDrawers.Add(effect);
    }

    public void DestroyAllEffectDrawers()
    {
        for (var i = EffectDrawers.Count - 1; i >= 0; i--)
        {
            Destroy(EffectDrawers[i].gameObject);
            EffectDrawers.RemoveAt(i);
        }
    }
}
