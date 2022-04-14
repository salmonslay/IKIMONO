using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Food,
    }

    public ItemType itemType;
    public string itemName;
    public Sprite sprite;
    public float foodValue;
    public int purchaseCost;

    public Sprite GetSprite()
    {
        return sprite;
    }

}
