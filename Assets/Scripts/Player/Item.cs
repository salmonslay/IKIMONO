using System.Linq;
using Newtonsoft.Json;
using ScriptableObjects;
using UnityEngine;

[JsonObject(MemberSerialization.OptIn)]
public class Item
{
    public ItemScriptableObject ItemObject => ItemScriptableObject.AllItems.FirstOrDefault(item => item.ItemName == _itemName);
    public string Name => ItemObject.ItemName;
    
    [JsonProperty("item")] private string _itemName;
    [JsonProperty("amount")] public int Amount;
    
    [JsonConstructor]
    public Item()
    {
        
    }
    public Item(string itemName, int amount = 1)
    {
        _itemName = itemName;
        Amount = amount;
    }
    
    public Item(ItemScriptableObject item, int amount = 1)
    {
        _itemName = item.ItemName;
        Amount = amount;
    }
    
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