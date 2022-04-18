using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{


    private Inventory _inventory;
    [SerializeField] private Transform _itemSlotContainer;
    [SerializeField] private Transform _itemSlot;

    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;
        inventory.OnItemListChanged += OnItemListChanged;

        RefreshInventoryItems();
    }

    // Triggas av uppdateringar i inventory.
    private void OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        // Tar bort alla itemslots förutom templaten.
        foreach (Transform child in _itemSlotContainer)
        {
            if (child == _itemSlot) continue;
            Destroy(child.gameObject);
        }

        // TODO: Byt ut mot UnityEngine.UI.GridLayoutGroup
        // Gå igenom alla items i inventory och uppdatera UI.
        const float itemSlotSize = 125f;
        int x = 0;
        int y = 0;
        foreach (Item item in _inventory.GetItemList())
        {
            // Skapa inventoryslot och placera ut i inventory.
            RectTransform itemSlotRectTransform = Instantiate(_itemSlot, _itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotSize, y * -itemSlotSize);

            // Sätt sprite till items sprite.
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            // Sätt amountText till antalet items i inventory ifall de finns fler än 1.
            Text amountText = itemSlotRectTransform.Find("AmountText").GetComponent<Text>();
            amountText.text = item.GetAmount() > 1 ? item.GetAmount().ToString() : "";

            // Lägg till referens till item i itemslot scriptet så att det går att anv�nda från UI.
            itemSlotRectTransform.GetComponent<ItemSlot>().SetUp(item, _inventory);

            // Uppdatera x och y koordinaterna så att nya items hamnar rätt i UI.
            x++;
            if (x >= 3) { x = 0; y++; }
        }
    }

}
