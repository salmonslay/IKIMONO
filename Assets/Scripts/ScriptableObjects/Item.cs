using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Food,
        Furniture,
    }

    public ItemType itemType;
    public string itemName;
    public Sprite sprite;
    public float foodValue;
    public int purchaseCost;
    public int amount;

    public Sprite GetSprite()
    {
        return sprite;
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Food:
                return true;
            case ItemType.Furniture:
                return false;
        }
    }

}
