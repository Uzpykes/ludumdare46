using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TileBehaviour : MonoBehaviour
{
    public UIResourceDrawer ResourceDrawer;
    public TextMeshProUGUI title;
    public BoardTile TileProperties;
    public RectTransform BuildCost;
    public RectTransform Production;
    public RectTransform Upkeep;

    public Image Image;

    public delegate void TilePlaced();
    public TilePlaced onTilePlaced;

    public void SetTileProperties(BoardTile tileProperties, Sprite sprite)
    {
        title.text = tileProperties.Name;
        TileProperties = tileProperties;
        Image.sprite = sprite;
    }

    public void OnBuild()
    {
        onTilePlaced();
        Destroy(BuildCost.gameObject);
    }
}
