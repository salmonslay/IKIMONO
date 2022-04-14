using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FoodItem", menuName = "ScriptableObjects/FoodItem")]
public class FoodItem : ScriptableObject
{
    public string foodName;
    public Sprite sprite;
    public float foodValue;
    public int purchaseCost;

}
