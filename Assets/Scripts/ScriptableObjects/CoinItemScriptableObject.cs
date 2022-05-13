using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Coin Item", menuName = "ScriptableObjects/Coins")]
    public class CoinItemScriptableObject : ScriptableObject
    {
        public string ItemName;
        public Sprite Sprite;
        public int coinAmount;
        public float realMoneyCost;

    }
}