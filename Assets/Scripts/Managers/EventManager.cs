using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public RectTransform EventCardPanel;
    public EventCardBehaviour EventCardPrefab;
    public UICardEffect EffectOnCardPrefab;
    public GameState GameState;
    public Graphic RaycastBlocker;

    public ResourceManager ResourceManager;

    public RectTransform CardHolder;

    public UnityEvent OnCardPicked;


    [System.NonSerialized]
    public List<EventCardBehaviour> ActiveCards = new List<EventCardBehaviour>();

    [SerializeField]
    private List<TurnEvent> EventPool;
    private Stack<TurnEvent> EventDeck = new Stack<TurnEvent>();

    private List<EventCardBehaviour> DrawnEventCards = new List<EventCardBehaviour>();

    public void OnEnable()
    {
        RaycastBlocker.raycastTarget = false;
        if (OnCardPicked == null)
            OnCardPicked = new UnityEvent();
    }

    public void Start()
    {
        BuildDeck();
    }

    public bool HasVictoryCondition()
    {
        return EventDeck.Count == 0 && ActiveCards.Count == 0;
    }

    public void BuildDeck()
    {
        while (EventPool.Count > 0)
        {
            var index = Random.Range(0, EventPool.Count);
            EventDeck.Push(EventPool[index]);
            EventPool.RemoveAt(index);
        }
    }

    public void SpawnEvent(TurnEvent ev)
    {
        var cardInstance = Instantiate(EventCardPrefab, EventCardPanel).GetComponent<EventCardBehaviour>();
        cardInstance.SetEventProperties(ev);
        DrawnEventCards.Add(cardInstance);
        DrawCardEffects(ev, cardInstance);

        //Register to click events
        if (cardInstance.TryGetComponent<ClickHandler>(out ClickHandler handler))
        {
            handler.OnClickEvent?.AddListener(TryPickCard);
        }
    }

    private void DrawCardEffects(TurnEvent ev, EventCardBehaviour cardInstance)
    {
        var resEffects = ev.ResourceEffects;
        foreach (var ef in resEffects)
        {
            var effectGroup = Instantiate(EffectOnCardPrefab, cardInstance.EffectsHolder).GetComponent<UICardEffect>();
            cardInstance.AddEffectDrawer(effectGroup);
            effectGroup.SetDescription(new List<string> { $"{ef.Duration} Years on {ef.AffectedValue}:" });
            foreach (var resType in ef.ResourceEffect)
            {
                var effectDrawer = Instantiate(EffectOnCardPrefab, cardInstance.EffectsHolder).GetComponent<UICardEffect>();
                cardInstance.AddEffectDrawer(effectDrawer);
                effectDrawer.DrawEffect(ef.AffectedTileTypes, resType);
            }
        }
        var drawEffects = ev.DrawEffects;
        if (drawEffects != null && drawEffects.Count > 0)
        {
            var effectGroup = Instantiate(EffectOnCardPrefab, cardInstance.EffectsHolder).GetComponent<UICardEffect>();
            cardInstance.AddEffectDrawer(effectGroup);
            effectGroup.SetDescription(new List<string> { $"Draw tiles:" });
            foreach (var drw in drawEffects)
            {
                var drawEffect = Instantiate(EffectOnCardPrefab, cardInstance.EffectsHolder).GetComponent<UICardEffect>();
                cardInstance.AddEffectDrawer(drawEffect);
                drawEffect.SetDescription(new List<string> { $"   {BoardTile.GetName(drw.Tile.Type)}" });
            }
        }
    }

    //Removes expired effects or whole card if nothing is left
    private void RemoveExpiredEffects()
    {
        for (var k = ActiveCards.Count - 1; k >= 0; k--)
        {
            var card = ActiveCards[k];
            var props = card.EventProperties;
            var resEffects = props.ResourceEffects;
            if (resEffects != null)
            {
                for (var i = resEffects.Count - 1; i >= 0; i--)
                {
                    if (resEffects[i].Duration + props.TurnDrawn <= GameState.CurrentTurn)
                    {
                        Debug.Log(card.Title.text);
                        resEffects.RemoveAt(i);
                    }
                }
            }

            if (resEffects == null || resEffects.Count == 0)
            {
                Destroy(card.gameObject);
                ActiveCards.RemoveAt(k);
            }
            else
            {
                card.DestroyAllEffectDrawers();
                DrawCardEffects(card.EventProperties, card);
            }
        }
    }

    public void CleanUp()
    {
        RemoveExpiredEffects();
    }

    //If card is good then add it to active cards list and destroy other cards
    public void TryPickCard(GameObject clickReceiver)
    {
        if (clickReceiver.TryGetComponent<EventCardBehaviour>(out EventCardBehaviour cardBehaviour))
        {
            StartCoroutine(TransitionToCardArea(cardBehaviour));
            OnCardSelected();
            while (DrawnEventCards.Count > 0)
            {
                var card = DrawnEventCards[0];
                DrawnEventCards.Remove(card);
                if (card != cardBehaviour)
                {
                    EventDeck.Push(card.EventProperties);
                    Destroy(card.gameObject);
                }
            }

        }
    }

    public IEnumerator TransitionToCardArea(EventCardBehaviour cardBehaviour)
    {
        ActiveCards.Add(cardBehaviour);
        if (cardBehaviour.TryGetComponent<ClickHandler>(out ClickHandler handler))
        {
            Destroy(handler);
        }
        cardBehaviour.transform.localScale *= 0.5f;
        cardBehaviour.transform.SetParent(CardHolder);
        cardBehaviour.EventProperties.TurnDrawn = GameState.CurrentTurn;
        OnCardPicked.Invoke();
        yield return new WaitForSeconds(0.1f); //hax lol, won't work if frame takes longer than 0.1f
        cardBehaviour.hoverHandler.Type = HoverReactionType.Shift;
        cardBehaviour.hoverHandler.MoveOnHover = new Vector3(0, 45, 0);
        ExecuteDrawEvents();
        RemoveExpiredEffects();
    }

    public void DrawEventCards()
    {
        RaycastBlocker.raycastTarget = true;
        EventCardPanel.gameObject.SetActive(true);

        while (DrawnEventCards.Count < 2 && EventDeck.Count > 0)
        {
            var turnEvent = EventDeck.Pop();
            SpawnEvent(turnEvent);
        }
        GameState.EventHandled = false;
        GameState.EventShown = true;
    }
    public void OnCardSelected()
    {
        GameState.EventHandled = false;
        GameState.EventShown = false;
        GameState.NextEventTurn += 3;
        RaycastBlocker.raycastTarget = false;
        EventCardPanel.gameObject.SetActive(false);
    }

    //Draws tiles
    public void ExecuteDrawEvents()
    {
        foreach (var activeCard in ActiveCards)
        {
            foreach (var drawEffect in activeCard.EventProperties.DrawEffects)
            {
                if (drawEffect.Executed == true)
                    continue;
                TileManager.instance.SpawnTile(drawEffect.Tile);
                drawEffect.Executed = true;
            }
        }
    }

    public List<DurationEffect> GetActiveEffects()
    {
        var activeEffects = new List<DurationEffect>();
        foreach (var activeCard in ActiveCards)
        {
            foreach (var drawEffect in activeCard.EventProperties.ResourceEffects)
            {
                activeEffects.Add(drawEffect);
            }
        }
        return activeEffects;
    }

}
