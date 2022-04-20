using IKIMONO.Pet;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{


    private Inventory _inventory;
    [SerializeField] private Transform _itemSlotContainer;
    [SerializeField] private GameObject _itemSlot;
    private Transform _itemSlotTransform;

    public void Start()
    {
        _inventory = Player.Instance.Inventory;
        _inventory.OnItemListChanged += OnItemListChanged;
        _itemSlotTransform = _itemSlot.transform;

        RefreshInventoryItems();
    }

    // Triggas av uppdateringar i inventory.
    private void OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        // Tar bort alla itemslots innan nya placeras ut.
        foreach (Transform child in _itemSlotContainer)
        {
            Destroy(child.gameObject);
        }

        // Gå igenom alla items i inventory och uppdatera UI.
        foreach (Item item in _inventory.GetItemList())
        {
            // Skapa inventoryslot och placera ut i inventory.
            RectTransform itemSlotRectTransform = Instantiate(_itemSlotTransform, _itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            // Sätt sprite till items sprite.
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            // Sätt amountText till antalet items i inventory ifall de finns fler än 1.
            Text amountText = itemSlotRectTransform.Find("AmountText").GetComponent<Text>();
            amountText.text = item.GetAmount() > 1 ? item.GetAmount().ToString() : "";

            // Lägg till referens till item i itemslot scriptet så att det går att anv�nda från UI.
            itemSlotRectTransform.GetComponent<ItemSlot>().SetUp(item, _inventory);
        }
    }

}
