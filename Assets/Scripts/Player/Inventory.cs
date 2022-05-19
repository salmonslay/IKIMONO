using System;
using System.Collections.Generic;
using IKIMONO.Pet;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class Inventory
{
    public event EventHandler OnItemListChanged;
    [JsonProperty("items")] public List<Item> ItemList { get; }

    public Inventory()
    {
        ItemList = new List<Item>();
    }

    public void AddItem(Item item, int amount)
    {
        // If the item is stackable and already exist in inventory add to it's amount, else
        // Add item to _itemList.
        if (item.IsStackable())
        {
            bool alreadyInInventory = false;
            foreach (Item inventoryItem in ItemList)
            {
                if (inventoryItem.ItemObject == item.ItemObject)
                {
                    inventoryItem.AddAmount(amount);
                    alreadyInInventory = true;
                }
            }
            if (!alreadyInInventory)
            {
                ItemList.Add(item);
                ItemList[ItemList.IndexOf(item)].AddAmount(amount - 1);
            }
        }
        // If the item is not stackable -> Just add to _itemList.
        else
        {
            ItemList.Add(item);
        }
        // Trigger OnItemListChanged to update UI.
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        Player.Instance.Save();
    }

    public void RemoveItem(Item item, int amount)
    {
        // If the item is stackable and exist in inventory -> Remove from amount.
        // If it was the last one left -> Remove it from _itemList.
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in ItemList)
            {
                if (inventoryItem.ItemObject == item.ItemObject)
                {
                    inventoryItem.RemoveAmount(amount);
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.GetAmount() <= 0)
            {
                ItemList.Remove(item);
            }
        }
        // If the item isn't stackable -> Remove from _itemList.
        else
        {
            ItemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        Player.Instance.Save();
    }

}
