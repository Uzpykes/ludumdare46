using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDeck : MonoBehaviour
{
    public void OnEnable()
    {
        TileDeck.CreateDeck();
    }

    public static BoardTile GetNextTile()
    {
        if (tiles == null)
            CreateDeck();
        if (tiles.Count > 0)
            return tiles.Pop();
        else 
            return null;
    }

    private static Stack<BoardTile> tiles;

    private static void CreateDeck()
    {
        var tileList = new List<BoardTile>();

        //Add fields
        for (var i = 0; i < 5; i++)
            tileList.Add(new BoardTile(TileType.Field, "Field", 
                new List<ResourceTuple>() { new ResourceTuple(ResourceType.Prosperity, 1)},
                new List<ResourceTuple>() { new ResourceTuple(ResourceType.Grain, 1) },
                null
            ));
        tileList.Add(new BoardTile(TileType.Field, "Magnificient Field", new List<ResourceTuple>() { new ResourceTuple(ResourceType.Prosperity, 3)},
            new List<ResourceTuple>() { new ResourceTuple(ResourceType.Grain, 2) },
            null
        ));
        // End add fields

        //Add lakes
        for (var i = 0; i < 3; i++)
            tileList.Add(new BoardTile(TileType.Lake, "Lake", new List<ResourceTuple>() { new ResourceTuple(ResourceType.Prosperity, 1)},
                new List<ResourceTuple>() { new ResourceTuple(ResourceType.Fish, 1) },
                null
            ));

        tileList.Add(new BoardTile(TileType.Lake, "Bottomless Lake", new List<ResourceTuple>() { new ResourceTuple(ResourceType.Prosperity, 3)},
            new List<ResourceTuple>() { new ResourceTuple(ResourceType.Fish, 2) },
            null
        ));
        //End add lake

        //Add roads
        for (var i = 0; i < 4; i++)
            tileList.Add(new BoardTile(TileType.Road, "Road", new List<ResourceTuple>() { new ResourceTuple(ResourceType.Prosperity, 1)},
                null,
                null
            ));
        //End add roads

        //Add forests
        for (var i = 0; i < 4; i++)
            tileList.Add(new BoardTile(TileType.Forest, "Forest", new List<ResourceTuple>() { new ResourceTuple(ResourceType.Prosperity, 1)},
                new List<ResourceTuple>(){new ResourceTuple(ResourceType.Wood, 1)},
                null
            ));

        tileList.Add(new BoardTile(TileType.Forest, "Uncrossable Forest", new List<ResourceTuple>() { new ResourceTuple(ResourceType.Prosperity, 3)},
            new List<ResourceTuple>(){new ResourceTuple(ResourceType.Wood, 3)},
            null
        ));
        //end add forests

        //Add mines
        for (var i = 0; i < 2; i++)
            tileList.Add(new BoardTile(TileType.Mine, " Mixed Mine", new List<ResourceTuple>() { new ResourceTuple(ResourceType.Prosperity, 2)},
                new List<ResourceTuple>(){new ResourceTuple(ResourceType.Clay, 1), new ResourceTuple(ResourceType.Iron, 1)},
                new List<ResourceTuple>(){new ResourceTuple(ResourceType.Bread, 1)}
            ));

        for (var i = 0; i < 2; i++)
            tileList.Add(new BoardTile(TileType.Mine, "Clay Mine", new List<ResourceTuple>() { new ResourceTuple(ResourceType.Prosperity, 2)},
                new List<ResourceTuple>(){new ResourceTuple(ResourceType.Clay, 2)},
                new List<ResourceTuple>(){new ResourceTuple(ResourceType.Bread, 1)}
            ));

        for (var i = 0; i < 2; i++)
            tileList.Add(new BoardTile(TileType.Mine, "Iron Mine", new List<ResourceTuple>() { new ResourceTuple(ResourceType.Prosperity, 2)},
                new List<ResourceTuple>(){new ResourceTuple(ResourceType.Iron, 2)},
                new List<ResourceTuple>(){new ResourceTuple(ResourceType.Bread, 1)}
            ));
        //end add forests

        tileList.Add(new BoardTile(TileType.Village, "Village 1", new List<ResourceTuple>() { new ResourceTuple(ResourceType.Prosperity, 3)},
            new List<ResourceTuple>(){new ResourceTuple(ResourceType.Bread, 3)},
            new List<ResourceTuple>(){new ResourceTuple(ResourceType.Bread, 1)}
        ));

        tileList.Add(new BoardTile(TileType.Village, "Village 2", new List<ResourceTuple>() { new ResourceTuple(ResourceType.Prosperity, 1)},
            new List<ResourceTuple>(){new ResourceTuple(ResourceType.Bread, 1), new ResourceTuple(ResourceType.Cloth, 1)},
            new List<ResourceTuple>(){new ResourceTuple(ResourceType.Bread, 1)}
        ));

        tileList.Add(new BoardTile(TileType.Village, "Village 3", new List<ResourceTuple>() { new ResourceTuple(ResourceType.Prosperity, 1)},
            new List<ResourceTuple>(){new ResourceTuple(ResourceType.Brick, 2)},
            new List<ResourceTuple>(){new ResourceTuple(ResourceType.Bread, 1), new ResourceTuple(ResourceType.Clay, 1)}
        ));

        tileList.Add(new BoardTile(TileType.SmolCity, "City 1", new List<ResourceTuple>() { new ResourceTuple(ResourceType.Prosperity, 3)},
            new List<ResourceTuple>(){new ResourceTuple(ResourceType.Brick, 4)},
            new List<ResourceTuple>(){new ResourceTuple(ResourceType.Bread, 3), new ResourceTuple(ResourceType.Clay, 2)}
        ));

        tileList.Add(new BoardTile(TileType.SmolCity, "City 2", new List<ResourceTuple>() { new ResourceTuple(ResourceType.Prosperity, 3)},
            new List<ResourceTuple>(){new ResourceTuple(ResourceType.Beer, 4)},
            new List<ResourceTuple>(){new ResourceTuple(ResourceType.Bread, 3), new ResourceTuple(ResourceType.Grain, 1)}
        ));

        tileList.Add(new BoardTile(TileType.SmolCity, "City 3", new List<ResourceTuple>() { new ResourceTuple(ResourceType.Prosperity, 3)},
            new List<ResourceTuple>(){new ResourceTuple(ResourceType.Cloth, 4)},
            new List<ResourceTuple>(){new ResourceTuple(ResourceType.Beer, 1)}
        ));

        tileList.Add(new BoardTile(TileType.LargeCity, "Large City 1", new List<ResourceTuple>() { new ResourceTuple(ResourceType.Prosperity, 3)},
            new List<ResourceTuple>(){new ResourceTuple(ResourceType.Prosperity, 4)},
            new List<ResourceTuple>(){new ResourceTuple(ResourceType.Beer, 2), new ResourceTuple(ResourceType.Iron, 1), new ResourceTuple(ResourceType.Grain, 2)}
        ));

        tileList.Add(new BoardTile(TileType.LargeCity, "Large City 2", new List<ResourceTuple>() { new ResourceTuple(ResourceType.Prosperity, 3)},
            new List<ResourceTuple>(){new ResourceTuple(ResourceType.Prosperity, 4), new ResourceTuple(ResourceType.Beer, 4)},
            new List<ResourceTuple>(){new ResourceTuple(ResourceType.Cloth, 2), new ResourceTuple(ResourceType.Iron, 2), new ResourceTuple(ResourceType.Grain, 2)}
        ));

        tiles = new Stack<BoardTile>();
        while(tileList.Count > 0)
        {
            var index = Random.Range(0, tileList.Count);
            var random = tileList[index];
            tiles.Push(random);
            tileList.RemoveAt(index);
        }
    }

    public static BoardTile GetCapitalTile()
    {
        return new BoardTile(TileType.Capital, "Capital", null,
            new List<ResourceTuple>(){new ResourceTuple(ResourceType.Grain, 1), new ResourceTuple(ResourceType.Prosperity, 1)},
            new List<ResourceTuple>(){new ResourceTuple(ResourceType.Grain, 1), new ResourceTuple(ResourceType.Bread, 1)});
    }

    

}
