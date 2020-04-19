using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileHolderBehaviour : MonoBehaviour
{
    public GameState GameState;
    public TileBehaviour HeldTile;
    public Vector2Int position;
    public Image DropImage; 

    private DropHandler handler;
    public void OnEnable()
    {
        handler = GetComponent<DropHandler>();
    }

    public void OnTryDrop()
    {
        var dragged = DragHandler.currentlyDraggedObject;
        //Evaluate if dragged item can be dropped
        if (dragged != null && dragged.TryGetComponent<TileBehaviour>(out TileBehaviour tileBehaviour))
        {
            if (ResourceManager.GetMissingResources(tileBehaviour.TileProperties.Cost).Count > 0)
                return; //Not enough resources to build
            
            ResourceManager.ConsumeResources(tileBehaviour.TileProperties.Cost, null);

            dragged.transform.SetParent(this.transform, false);
            dragged.transform.localPosition = Vector3.zero;
            if (DragHandler.currentlyDraggedObject.TryGetComponent<DragHandler>(out DragHandler dragHandler))
            {
                Destroy(dragHandler);
            }
            if (DragHandler.currentlyDraggedObject.TryGetComponent<HoverHandler>(out HoverHandler hoverHandler))
            {
                Destroy(hoverHandler);
            }
            HeldTile = tileBehaviour;
            HeldTile.OnBuild();
            if (HeldTile.TileProperties.Type == TileType.Capital)
                OnCapitalPlaced();
            GameBoardManager.RecalculateDroppable(this);
        }
    }

    public void SetAcceptsDrops(bool val)
    {
        if (handler != null)
            handler.enabled = val;
    }

    private void OnCapitalPlaced()
    {
        if (GameState.CapitalPlaced == true)
        {
            Debug.LogError("Capital was already placed before!");
            return;
        }
        GameState.CapitalPlaced = true;
    }
}
