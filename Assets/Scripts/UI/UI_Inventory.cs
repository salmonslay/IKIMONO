using IKIMONO.Pet;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{


    private Inventory _inventory;
    [SerializeField] private Transform _itemSlotContainer;
    [SerializeField] private GameObject _itemSlot;

    public void Start()
    {
        _inventory = Player.Instance.Inventory;
        _inventory.OnItemListChanged += OnItemListChanged;

        RefreshInventoryItemsIU();
    }

    // Subscribes and refreshes UI on event.
    private void OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItemsIU();
    }

    private void RefreshInventoryItemsIU()
    {
        // Return out if UI_Window is inactive.
        if (_itemSlotContainer == null)
        {
            return;
        }

        // Remove all old itemslots.
        for (int i = _itemSlotContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(_itemSlotContainer.GetChild(i).gameObject);
        }

        // Loop through all items in inventory.
        foreach (Item item in _inventory.ItemList)
        {
            // Instantiate a new inteslot.
            GameObject itemSlot = Instantiate(_itemSlot, _itemSlotContainer);
            itemSlot.gameObject.SetActive(true);

            // Set item sprite.
            Image image = itemSlot.transform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            // Set item amount in textslot.
            Text amountText = itemSlot.transform.Find("AmountText").GetComponent<Text>();
            amountText.text = item.GetAmount() > 1 ? item.GetAmount().ToString() : "";

            // Add a reference to the item in the slot.
            itemSlot.GetComponent<ItemSlot>().Item = item;
        }
    }

}
