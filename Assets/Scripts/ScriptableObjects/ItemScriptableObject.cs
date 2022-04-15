using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemScriptableObject")]
public class ItemScriptableObject : ScriptableObject
{

    public Item.ItemType ItemType;
    public string ItemName;
    public Sprite Sprite;
    public float FoodValue;
    public int PurchaseCost;

}
