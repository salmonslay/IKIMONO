using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseItemButton : MonoBehaviour
{
    [SerializeField] private ItemScriptableObject _itemScriptableObject;
    private Inventory _inventory;

    private void Start()
    {
        _inventory = PlayerTestClass.Instance.GetInventory(); ;

        Text _text = transform.Find("Text").GetComponent<Text>();
        _text.text = "Buy: " + _itemScriptableObject.ToString();
    }

    public void BuyItem()
    {
        _inventory.AddItem(new Item { ItemObject = _itemScriptableObject }, 1);
    }
}
