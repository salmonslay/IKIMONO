using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseItemButton : MonoBehaviour
{
    [SerializeField] private ItemScriptableObject itemScriptableObject;
    private Inventory inventory;

    private void Start()
    {
        inventory = PlayerTestClass.Instance.GetInventory(); ;

        Text text = transform.Find("Text").GetComponent<Text>();
        text.text = "Buy: " + itemScriptableObject.ToString();
    }

    public void BuyItem()
    {
        inventory.AddItem(new Item { ItemScriptableObject = itemScriptableObject }, 1);
    }
}
