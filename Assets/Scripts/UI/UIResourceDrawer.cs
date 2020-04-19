using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIResourceDrawer : MonoBehaviour
{

    public Resource DataSource;
    [SerializeField]
    private Image Icon;
    [SerializeField]
    private TextMeshProUGUI Quantity;

    public void SetDataSource(Resource resource)
    {
        if (DataSource != null)
        {
            Debug.LogError("Data source was already set!");
            return;
        }
        DataSource = resource;
        UpdateUI();
        DataSource.ResourceInfo.onCountChanged += UpdateUI;
    }

    private void OnDestroy()
    {
        if (DataSource != null)
            DataSource.ResourceInfo.onCountChanged -= UpdateUI;
    }

    public void SetSprite(Sprite sprite)
    {
        Icon.sprite = sprite;
    }

    public void SetValue(string value)
    {
        Quantity.text = value;
    }

    public void UpdateUI()
    {
        Icon.sprite = DataSource.ResourceSprite;
        Quantity.text = DataSource.ResourceInfo.Count.ToString();
    }

    //can add other stuff like hovering etc.
}
