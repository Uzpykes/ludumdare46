using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    public GameState GameState;
    public TileBehaviour TilePrefab;
    public static TileManager instance;
    public void OnEnable()
    {
        instance = this;
        if (OnTilePlaced == null)
            OnTilePlaced = new UnityEvent();
    }

    public List<Resource> Resources = new List<Resource>();
    public List<TileSpriteMapping> TileSpriteMapping = new List<TileSpriteMapping>();
    public UIResourceDrawer ResourceDrawer;

    private List<TileBehaviour> PlaceableTileInstances = new List<TileBehaviour>();
    public RectTransform TileArea;

    public ResourceManager ResourceManager;

    public UnityEvent OnTilePlaced;


    public void SpawnTilesIfNeeded()
    {   
        if (PlaceableTileInstances.Count >= 3)
            return;

        if (!GameState.CapitalSpawned)
        {
            var tileProperties = TileDeck.GetCapitalTile();
            SpawnTile(tileProperties);
            GameState.CapitalSpawned = true;
            return;
        }

        for (var i = PlaceableTileInstances.Count; i < 3; i++)
        {
            var tileProperties = TileDeck.GetNextTile();
            SpawnTile(tileProperties);
        }
    }

    public void SpawnTile(BoardTile tileProperties)
    {
        var instance = Instantiate(TilePrefab.gameObject, this.transform).GetComponent<TileBehaviour>();
        var tileSprite = TileSpriteMapping.Find(x => {return x.Type == tileProperties.Type;}).Sprite;
        instance.SetTileProperties(tileProperties, tileSprite);

        if (tileProperties.Cost != null)
        {
            foreach(var cost in tileProperties.Cost)
            {
                var sprite = ResourceManager.Resources.Find(x => { return x.ResourceInfo.Type == cost.Type; })?.ResourceSprite;
                var resDrawer = Instantiate(ResourceDrawer.gameObject, instance.BuildCost).GetComponent<UIResourceDrawer>();
                resDrawer.SetSprite(sprite);
                resDrawer.SetValue(cost.Count.ToString());
            }
        }

        if (tileProperties.Production != null)
        {
            foreach(var prod in tileProperties.Production)
            {
                var sprite = ResourceManager.Resources.Find(x => { return x.ResourceInfo.Type == prod.Type; }).ResourceSprite;
                var resDrawer = Instantiate(ResourceDrawer.gameObject, instance.Production).GetComponent<UIResourceDrawer>();
                resDrawer.SetSprite(sprite);
                resDrawer.SetValue(prod.Count.ToString());
            }
        }

        if (tileProperties.Upkeep != null)
        {
            foreach(var upkeep in tileProperties.Upkeep)
            {
                var sprite = ResourceManager.Resources.Find(x => { return x.ResourceInfo.Type == upkeep.Type; })?.ResourceSprite;
                var resDrawer = Instantiate(ResourceDrawer.gameObject, instance.Upkeep).GetComponent<UIResourceDrawer>();
                resDrawer.SetSprite(sprite);
                resDrawer.SetValue(upkeep.Count.ToString());
            }
        }

        PlaceableTileInstances.Add(instance);
        instance.onTilePlaced += () => {
            PlaceableTileInstances.Remove(instance);
            OnTilePlaced.Invoke();
        };
    }
}

[System.Serializable]
public class TileSpriteMapping
{
    public TileType Type;
    public Sprite Sprite;
}
