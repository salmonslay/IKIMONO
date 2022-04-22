using IKIMONO.Pet;
using ScriptableObjects;
using UnityEngine;

public class PurchaseItemButton : MonoBehaviour
{
    public ItemScriptableObject ItemObject;


    public void BuyItem()
    {
        Player player = Player.Instance;
        int itemCost = ItemObject.PurchaseCost;
        if (player.Coins > itemCost)
        {
            player.RemoveCoins(itemCost);
            player.Inventory.AddItem(new Item(ItemObject), 1);
        }
    }
}
