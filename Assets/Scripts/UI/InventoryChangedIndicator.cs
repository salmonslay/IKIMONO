using IKIMONO.Pet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryChangedIndicator : MonoBehaviour
{
    Text text;
    int changeCount = 0;

    void Start()
    {
        Player.Instance.Inventory.OnItemListChanged += OnItemListChanged;
        text = gameObject.GetComponentInChildren<Text>();
        gameObject.SetActive(false);
    }

    private void OnItemListChanged(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        changeCount++;
        text.text = (changeCount < 100) ? changeCount.ToString() : 99.ToString();
    }

    private void OnDestroy()
    {
        Player.Instance.Inventory.OnItemListChanged -= OnItemListChanged;
    }

    public void ResetChangeIndicator()
    {
        changeCount = 0;
        gameObject.SetActive(false);
    }
}
