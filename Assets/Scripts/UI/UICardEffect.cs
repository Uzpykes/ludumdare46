using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICardEffect : MonoBehaviour
{
    public TextMeshProUGUI Description;
    public UIResourceDrawer ResourceDrawer;

    public void DrawEffect(List<TileType> affectedTypes, ResourceTuple resource)
    {
        Description.text = "   ";
        for (var i = 0; i < affectedTypes.Count; i++) {
            Description.text += BoardTile.GetName(affectedTypes[i]);
            if (i != affectedTypes.Count - 1)
                Description.text += ", ";
        }
        if (resource != null)
        {
            var drawer = Instantiate(ResourceDrawer.gameObject, transform).GetComponent<UIResourceDrawer>();
            drawer.SetSprite(ResourceManager.instance.Resources.Find(x => {return x.ResourceInfo.Type == resource.Type;}).ResourceSprite);
            drawer.SetValue(resource.Count.ToString());
        }
    }

    public void SetDescription(List<string> strings)
    {
        Description.text = "";
        for (var i = 0; i < strings.Count; i++) {
            Description.text += strings[i];
        }
    }

}
