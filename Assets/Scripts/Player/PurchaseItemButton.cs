using IKIMONO.Pet;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseItemButton : MonoBehaviour
{
    [SerializeField] private ItemScriptableObject _itemScriptableObject;

    private void Start()
    {
        Text text = transform.Find("Text").GetComponent<Text>();
        text.text = "Buy: " + _itemScriptableObject.ItemName;
    }

    public void BuyItem()
    {
        Player.Instance.Inventory.AddItem(new Item(_itemScriptableObject), 1);
        // TODO: reduce coins? is this done elsewhere?

        Player.Instance.Save();
    }
}
