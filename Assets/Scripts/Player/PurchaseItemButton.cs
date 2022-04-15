using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseItemButton : MonoBehaviour
{
    [SerializeField] private Item item;
    private Inventory inventory;

    private void Start()
    {
        inventory = Player.Instance.GetInventory(); ;

        Text text = transform.Find("Text").GetComponent<Text>();
        text.text = "Buy: " + item.itemName;
    }

    public void BuyItem()
    {
        inventory.AddItem(item, 1);
    }
}
