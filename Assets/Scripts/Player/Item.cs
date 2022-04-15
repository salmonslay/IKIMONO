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
    private int _amount;

    public Sprite GetSprite()
    {
        return ItemScriptableObject.Sprite;
    }

    public bool IsStackable()
    {
        switch (ItemScriptableObject.ItemType)
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
        return ItemScriptableObject.ItemName;
    }

    public int GetAmount()
    {
        return _amount;
    }

    public void AddAmount(int addedValue)
    {
        _amount += addedValue;
    }

    public void RemoveAmount(int removedAmount)
    {
        _amount -= removedAmount;
    }
}
