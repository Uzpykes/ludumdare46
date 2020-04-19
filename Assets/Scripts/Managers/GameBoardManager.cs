using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoardManager : MonoBehaviour
{
    public static GameBoardManager instance;
    public void OnEnable()
    {
        if (instance == null)
            instance = this;
    }
    public GridLayoutGroup Container;
    public static Vector2Int BoardDimensions = new Vector2Int(7, 7);
    public TileHolderBehaviour TileHolderPrefab;
    private static List<TileHolderBehaviour> tileHolders = new List<TileHolderBehaviour>();
    public GameState GameState;

    public TileManager TileManager;
    public EventManager EventManager;
    public EndGameManager EndGameManager;

    public void Awake()
    {
        Container.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        Container.constraintCount = BoardDimensions.x;
        for (var i = 0; i < BoardDimensions.x * BoardDimensions.y; i++)
        {
            var holder = Instantiate(TileHolderPrefab.gameObject, Container.transform).GetComponent<TileHolderBehaviour>();
            holder.position = new Vector2Int(i % BoardDimensions.x, i / BoardDimensions.y);
            tileHolders.Add(holder);
        }
    }

    public void TryAdvance()
    {
        if (EventManager.HasVictoryCondition())
        {
            GameState.Victory = true;
            var prospVal =  ResourceManager.instance.Resources.Find(x => {return x.ResourceInfo.Type == ResourceType.Prosperity; }).ResourceInfo.Count;
            EndGameManager.SetResult("Victory!", $"You've finised with {prospVal} <b>Prosperity</b>");
            EndGameManager.Show();
            return;
        }
        if (CheckIfNeedsEvent())
        {
            EventManager.DrawEventCards();
        }
        else
        {
            GameState.CurrentTurn++;
            TileManager.SpawnTilesIfNeeded();
            ApplyDurationEffects();
            CalculateProduction();
            CalculateUpkeep();
            EventManager.CleanUp();
            //Check if can go to next turn
            //Apply events
            //Calculate resources
            //Calculate modifiers
        }
        if (ResourceManager.instance.HasLooseCondition())
        {
            GameState.Loss = true;
            EndGameManager.SetResult("Defeat.", $"You've lost on <b>Year {GameState.CurrentTurn}</b>");
            EndGameManager.Show();
            return;
        }
    }

    public bool CheckIfNeedsEvent()
    {
        if (GameState.CurrentTurn == GameState.NextEventTurn && !GameState.EventShown)
            return true;
        return false;
    }

    public void ApplyDurationEffects()
    {
        var currentEffects = EventManager.GetActiveEffects();
        foreach (var holder in tileHolders)
        {
            var tile = holder.HeldTile?.TileProperties;
            if (tile == null)
                continue;

            tile.EffectsOnProduction = new List<ResourceTuple>();
            tile.EffectsOnUpkeep = new List<ResourceTuple>();

            foreach (var effect in currentEffects)
            {
                //if this effect applies to tile type
                if (effect.AffectedTileTypes.Contains(tile.Type))
                {
                    if (effect.AffectedValue == AffectedValue.Production)
                    {
                        tile.EffectsOnProduction.AddRange(effect.ResourceEffect);
                    }
                    else if (effect.AffectedValue == AffectedValue.Upkeep)
                    {
                        tile.EffectsOnUpkeep.AddRange(effect.ResourceEffect);
                    }
                }
            }
        }
    }

    public void CalculateProduction()
    {
        //This gets all tiles into two lists and does calculation on all tiles at once
        var prod = new List<ResourceTuple>();
        var eff = new List<ResourceTuple>();
        foreach (var holder in tileHolders)
        {
            if (holder.HeldTile != null && holder.HeldTile.TileProperties.Production != null)
                prod.AddRange(holder.HeldTile.TileProperties.Production);
            if (holder.HeldTile != null && holder.HeldTile.TileProperties.EffectsOnProduction != null)
                eff.AddRange(holder.HeldTile.TileProperties.EffectsOnProduction);
        }
        ResourceManager.ProduceResources(prod, eff);
    }

    public void CalculateUpkeep()
    {
        //This gets all tiles into two lists and does calculation on all tiles at once
        var upkeep = new List<ResourceTuple>();
        var eff = new List<ResourceTuple>();
        foreach (var holder in tileHolders)
        {
            if (holder.HeldTile != null && holder.HeldTile.TileProperties.Upkeep != null)
                upkeep.AddRange(holder.HeldTile.TileProperties.Upkeep);
            if (holder.HeldTile != null && holder.HeldTile.TileProperties.EffectsOnUpkeep != null)
                eff.AddRange(holder.HeldTile.TileProperties.EffectsOnUpkeep);
        }
        ResourceManager.ConsumeResources(upkeep, eff);
    }


    public void Start()
    {
        //Set starting prosperity to 6
        ResourceManager.instance.Resources.Find(x => { return x.ResourceInfo.Type == ResourceType.Prosperity; }).ResourceInfo.Count = 6;
        ResourceManager.instance.Resources.Find(x => { return x.ResourceInfo.Type == ResourceType.Bread; }).ResourceInfo.Count = 2;
        TileManager.SpawnTilesIfNeeded();
        EventManager.OnCardPicked.AddListener(ApplyDurationEffects);
        TileManager.OnTilePlaced.AddListener(ApplyDurationEffects); //this goes through every tile
    }

    public static void RecalculateDroppable(TileHolderBehaviour holder)
    {
        //if this was capital tile then make all tiles as not droppable
        if (holder.HeldTile.TileProperties.Type == TileType.Capital)
        {
            foreach (var h in tileHolders)
            {
                h.SetAcceptsDrops(false);
            }
        }

        holder.SetAcceptsDrops(false);

        if (holder.position.x > 0)
            tileHolders.Find(x => (x.HeldTile == null && x.position.x == holder.position.x - 1 && x.position.y == holder.position.y))?.SetAcceptsDrops(true);
        if (holder.position.x < BoardDimensions.x - 1)
            tileHolders.Find(x => (x.HeldTile == null && x.position.x == holder.position.x + 1 && x.position.y == holder.position.y))?.SetAcceptsDrops(true);

        if (holder.position.y > 0)
            tileHolders.Find(x => (x.HeldTile == null && x.position.x == holder.position.x && x.position.y == holder.position.y - 1))?.SetAcceptsDrops(true);
        if (holder.position.y < BoardDimensions.y - 1)
            tileHolders.Find(x => (x.HeldTile == null && x.position.x == holder.position.x && x.position.y == holder.position.y + 1))?.SetAcceptsDrops(true);
    }
}
