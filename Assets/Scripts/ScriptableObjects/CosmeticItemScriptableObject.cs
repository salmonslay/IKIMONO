using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Cosmetic")]
public class CosmeticItemScriptableObject : ScriptableObject
{
    public readonly Item.ItemType ItemType = Item.ItemType.Cosmetic;
    public string ItemName;
    public Sprite Sprite;
    public int PurchaseCost;
}
