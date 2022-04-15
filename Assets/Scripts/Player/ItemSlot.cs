using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public Item item;
    private Inventory inventory;

    public void SetUp(Item item, Inventory inventory)
    {
        this.item = item;
        this.inventory = inventory;
    }

    public Item GetItem()
    {
        return item;
    }

    public void UseItem()
    {
        inventory.RemoveItem(item, 1);
    }
}
