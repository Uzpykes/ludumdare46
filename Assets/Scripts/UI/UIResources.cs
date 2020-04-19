using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResources : MonoBehaviour
{
    public List<Resource> Resources;
    public UIResourceDrawer DrawerPrefab;

    public void Awake()
    {
        foreach(var resource in Resources)
        {
            var o = Instantiate(DrawerPrefab.gameObject, transform);
            o.GetComponent<UIResourceDrawer>().SetDataSource(resource);
        }
    }

}
