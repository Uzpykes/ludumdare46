using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{
    public string EffectName;
}

//Adds a specific tile
[System.Serializable]
public class DrawEffect : Effect
{
    public BoardTile Tile;
    public bool Executed = false;
    public DrawEffect(BoardTile tile)
    {
        Tile = tile;
    }
}

[System.Serializable]
public class DurationEffect : Effect
{
    public List<ResourceTuple> ResourceEffect;
    public AffectedValue AffectedValue;
    public List<TileType> AffectedTileTypes;
    public int Duration;
    [System.NonSerialized]
    public int StartTurn;
    public DurationEffect(List<ResourceTuple> resourceEffect, int duration, int startTurn)
    {
        ResourceEffect = resourceEffect;
        Duration = duration;
        StartTurn = startTurn;
    }
}

public enum AffectedValue
{
    Production,
    Upkeep,
    Cost
}