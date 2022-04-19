using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using ScriptableObjects;
using UnityEngine;

[JsonObject(MemberSerialization.OptIn)]
public class Item
{
    public ItemScriptableObject ItemObject;
    public string Name => ItemObject.ItemName;
    
    [JsonProperty("item")] private string _itemName;
    [JsonProperty("amount")] public int Amount;

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
        return Amount;
    }

    public void AddAmount(int addedValue)
    {
        Amount += addedValue;
    }

    public void RemoveAmount(int removedAmount)
    {
        Amount -= removedAmount;
    }

}