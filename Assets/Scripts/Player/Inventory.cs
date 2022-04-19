using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[JsonObject(MemberSerialization.OptIn)]
public class Inventory
{
    public event EventHandler OnItemListChanged;

    [JsonProperty("items")] private List<Item> _itemList;

    public Inventory()
    {
        _itemList = new List<Item>();
    }

    public void AddItem(Item item, int amount)
    {
        //Om item kan stacka -> Hitta i listan och addera på amount
        //Finns item inte i listan, lägg till
        if (item.IsStackable())
        {
            bool alreadyInInventory = false;
            foreach (Item inventoryItem in _itemList)
            {
                if (inventoryItem.ItemObject == item.ItemObject)
                {
                    inventoryItem.AddAmount(amount);
                    alreadyInInventory = true;
                }
            }
            if (!alreadyInInventory)
            {
                item.AddAmount(amount);
                _itemList.Add(item);
            }
        }
        else
        {
            item.AddAmount(amount);
            _itemList.Add(item);
        }
        //Trigga förändring i UI
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item, int amount)
    {
        //Om item kan stacka -> Hitta i listan och substrahera på amount
        //Fanns item i listan och är nu under 1, ta bort ur listan
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in _itemList)
            {
                if (inventoryItem.ItemObject == item.ItemObject)
                {
                    inventoryItem.RemoveAmount(amount);
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.GetAmount() <= 0)
            {
                _itemList.Remove(item);
            }
        }
        else
        {
            _itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return _itemList;
    }
}
