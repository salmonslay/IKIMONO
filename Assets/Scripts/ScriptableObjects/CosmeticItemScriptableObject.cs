using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemScriptableObject")]
public class CosmeticItemScriptableObject : ScriptableObject
{

    public Item.ItemType ItemType = Item.ItemType.Cosmetic;
    public string ItemName;
    public Sprite Sprite;
    public int PurchaseCost;

}
