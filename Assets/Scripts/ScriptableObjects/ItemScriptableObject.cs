using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemScriptableObject")]
public class ItemScriptableObject : ScriptableObject
{

    public Item.ItemType itemType;
    public string itemName;
    public Sprite sprite;
    public float foodValue;
    public int purchaseCost;

}
