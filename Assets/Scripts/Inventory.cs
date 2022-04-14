using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
    }

    public void AddItem(Item item, int amount)
    {
        if (item.IsStackable())
        {
            bool alreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemName == item.itemName)
                {
                    inventoryItem.amount += amount;
                    alreadyInInventory = true;
                }
            }
            if (!alreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item, int amount)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemName == item.itemName)
                {
                    inventoryItem.amount -= amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(item);
            }
        }
        else
        {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
