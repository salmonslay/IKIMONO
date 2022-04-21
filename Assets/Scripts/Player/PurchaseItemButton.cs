using IKIMONO.Pet;
using ScriptableObjects;
using UnityEngine;

public class PurchaseItemButton : MonoBehaviour
{
    public ItemScriptableObject ItemObject;


    public void BuyItem()
    {
        Player.Instance.Inventory.AddItem(new Item(ItemObject), 1);
    }
}
