using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemScriptableObject")]
public class FoodItemScriptableObject : ScriptableObject
{

    public Item.ItemType ItemType = Item.ItemType.Food;
    public string ItemName;
    public Sprite Sprite;
    public float FoodValue;
    public int PurchaseCost;

}
