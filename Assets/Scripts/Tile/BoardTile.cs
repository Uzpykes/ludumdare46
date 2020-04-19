using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoardTile
{
    public TileType Type;
    public string Name;
    public List<ResourceTuple> Cost;

    public List<ResourceTuple> Production;
    public List<ResourceTuple> Upkeep;

    public List<ResourceTuple> EffectsOnProduction;
    public List<ResourceTuple> EffectsOnUpkeep;

    public BoardTile(TileType type, string name, List<ResourceTuple> cost, List<ResourceTuple> production, List<ResourceTuple> upkeep)
    {
        Type = type;
        Name = name;
        Production = production;
        Upkeep = upkeep;
        Cost = cost;
    }
   

    public static string GetName(TileType type)
    {
        switch(type)
        {
            case TileType.Field:
                return "Field";
            case TileType.Road:
                return "Road";
            case TileType.Lake:
                return "Lake";
            case TileType.Forest:
                return "Forest";
            case TileType.Mine:
                return "Mine";
            case TileType.Village:
                return "Village";
            case TileType.SmolCity:
                return "Small City";
            case TileType.LargeCity:
                return "Large City";
            case TileType.Capital:
                return "The Capital";
            case TileType.Battlefield:
                return "Battlefield";
            case TileType.RottingField:
                return "Rotting Field";
            case TileType.Swamp:
                return "Swamp";
            case TileType.ForestFire:
                return "Forest Fire";
            default:
                return "[unknown]";
        }
    }
}

public enum TileType
{
    Field,
    Road,
    Lake,
    Forest,
    Mine,
    Village,
    SmolCity,
    LargeCity,
    Capital,

    //From events
    Battlefield,
    RottingField,
    Swamp,
    ForestFire
}