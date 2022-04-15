using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Item _item;
    private Inventory _inventory;

    public void SetUp(Item item, Inventory inventory)
    {
        _item = item;
        _inventory = inventory;
    }

    public Item GetItem()
    {
        return _item;
    }

    public void UseItem()
    {
        _inventory.RemoveItem(_item, 1);
    }
}
