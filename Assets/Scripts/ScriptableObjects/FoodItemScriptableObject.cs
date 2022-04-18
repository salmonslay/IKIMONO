using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Food", menuName = "ScriptableObjects/Food")]
public class FoodItemScriptableObject : ScriptableObject
{
    public readonly Item.ItemType ItemType = Item.ItemType.Food;
    public string ItemName;
    public Sprite Sprite;
    public float FoodValue;
    public float EnergyValue;
    public int PurchaseCost;
}
