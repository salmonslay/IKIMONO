using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using ScriptableObjects;
using UnityEngine;

[JsonObject(MemberSerialization.OptIn)]
public class Item
{
    public ItemScriptableObject ItemObject;

    [JsonProperty("item")] private string _itemName => ItemObject.ItemName;
    [JsonProperty("amount")] private int _amount;

    public Sprite GetSprite()
    {
        return ItemObject.Sprite;
    }

    public bool IsStackable()
    {
        if (ItemObject.GetType() == typeof(FoodItemScriptableObject))
            return true;
        else if (ItemObject.GetType() == typeof(CosmeticItemScriptableObject))
            return false;

        return false;
    }
    
    public override string ToString()
    {
        return ItemObject.ItemName;
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
