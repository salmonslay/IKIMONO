using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[JsonObject(MemberSerialization.OptIn)]
public class Item
{
    public enum ItemType
    {
        Food,
        Furniture,
    }

    public ItemScriptableObject ItemScriptableObject;

    [JsonProperty("item")] private string _itemName => ItemScriptableObject.ItemName;
    [JsonProperty("amount")] private int _amount;

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

    
    public override string ToString()
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
