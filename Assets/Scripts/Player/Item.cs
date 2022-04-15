using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Food,
        Furniture,
    }

    public ItemScriptableObject ItemScriptableObject;
    private int amount;

    public Sprite GetSprite()
    {
        return ItemScriptableObject.sprite;
    }

    public bool IsStackable()
    {
        switch (ItemScriptableObject.itemType)
        {
            default:
            case ItemType.Food:
                return true;
            case ItemType.Furniture:
                return false;
        }
    }

    override
    public string ToString()
    {
        return ItemScriptableObject.itemName;
    }

    public int GetAmount()
    {
        return amount;
    }

    public void AddAmount(int addedValue)
    {
        amount += addedValue;
    }

    public void RemoveAmount(int removedAmount)
    {
        amount -= removedAmount;
    }
}
