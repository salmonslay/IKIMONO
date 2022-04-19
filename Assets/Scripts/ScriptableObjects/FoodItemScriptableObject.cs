using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;


[CreateAssetMenu(fileName = "New Food", menuName = "ScriptableObjects/Food")]
public class FoodItemScriptableObject : ItemScriptableObject
{
    public float HungerValue;
    public float EnergyValue;
}
