using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{


    private Inventory inventory;
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlot;

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        Debug.Log("UI script reached");
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlot) continue;
            Destroy(child.gameObject);
        }

        float itemSlotSize = 125f;
        int x = 0;
        int y = 0;


        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlot, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotSize, y * -itemSlotSize);

            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            Text amountText = itemSlotRectTransform.Find("AmountText").GetComponent<Text>();
            if (item.amount > 1)
            {
                amountText.text = item.amount.ToString();
            }
            else
            {
                amountText.text = "";
            }

            itemSlotRectTransform.GetComponent<ItemSlot>().SetUp(item, inventory);

            x++;
            if (x >= 3) { x = 0; y++; }
        }
    }

}
