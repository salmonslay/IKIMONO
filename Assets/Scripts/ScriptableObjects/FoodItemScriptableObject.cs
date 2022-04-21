using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;


[CreateAssetMenu(fileName = "New Food", menuName = "ScriptableObjects/Food")]
public class FoodItemScriptableObject : ItemScriptableObject
{
    public float FunValue;
    public float HungerValue;
    public float HygieneValue;
    public float EnergyValue;
    public float SocialValue;
}
