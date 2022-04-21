using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    [SerializeField] private List<ScriptableObjects.ItemScriptableObject> itemList;

    [SerializeField] private GameObject _shopItemSlot;
    [SerializeField] private GameObject _infoRow;

    [SerializeField] private Sprite _funSprite;
    [SerializeField] private Sprite _hungerSprite;
    [SerializeField] private Sprite _hygieneSprite;
    [SerializeField] private Sprite _energySprite;
    [SerializeField] private Sprite _socialSprite;

    private void Start()
    {
        foreach (ScriptableObjects.ItemScriptableObject itemObject in itemList)
        {
            GameObject shopItemSlot = Instantiate(_shopItemSlot, GameObject.Find("ItemShopViewContent").transform);
            SetupItemSlot(itemObject, shopItemSlot);
        }
    }

    private void SetupItemSlot(ScriptableObjects.ItemScriptableObject itemObject, GameObject shopItemSlot)
    {
        ScriptableObjects.ItemScriptableObject itemScriptableObject = itemObject;
        //Setup Image
        shopItemSlot.transform.Find("ItemImageContainer").Find("ItemImage").GetComponent<Image>().sprite = itemScriptableObject.Sprite;

        //Setup ItemInfo
        GameObject itemInfo = shopItemSlot.transform.Find("ItemInfo").gameObject;
        if (itemScriptableObject.GetType() == typeof(FoodItemScriptableObject))
        {
            FoodItemScriptableObject foodItemScriptableObject = (FoodItemScriptableObject)itemScriptableObject;
            if (foodItemScriptableObject.FunValue > 0)
            {
                GameObject itemInfoRow = Instantiate(_infoRow, itemInfo.transform);
                itemInfoRow.GetComponentInChildren<Image>().sprite = _funSprite;
                itemInfoRow.GetComponentInChildren<Text>().text = "+ " + foodItemScriptableObject.FunValue;
            }
            if (foodItemScriptableObject.HungerValue > 0)
            {
                GameObject itemInfoRow = Instantiate(_infoRow, itemInfo.transform);
                itemInfoRow.GetComponentInChildren<Image>().sprite = _hungerSprite;
                itemInfoRow.GetComponentInChildren<Text>().text = "+ " + foodItemScriptableObject.HungerValue;

            }
            if (foodItemScriptableObject.HygieneValue > 0)
            {
                GameObject itemInfoRow = Instantiate(_infoRow, itemInfo.transform);
                itemInfoRow.GetComponentInChildren<Image>().sprite = _hygieneSprite;
                itemInfoRow.GetComponentInChildren<Text>().text = "+ " + foodItemScriptableObject.HygieneValue;

            }
            if (foodItemScriptableObject.EnergyValue > 0)
            {
                GameObject itemInfoRow = Instantiate(_infoRow, itemInfo.transform);
                itemInfoRow.GetComponentInChildren<Image>().sprite = _energySprite;
                itemInfoRow.GetComponentInChildren<Text>().text = "+ " + foodItemScriptableObject.EnergyValue;

            }
            if (foodItemScriptableObject.SocialValue > 0)
            {
                GameObject itemInfoRow = Instantiate(_infoRow, itemInfo.transform);
                itemInfoRow.GetComponentInChildren<Image>().sprite = _socialSprite;
                itemInfoRow.GetComponentInChildren<Text>().text = "+ " + foodItemScriptableObject.SocialValue;
            }
        }

        //Setup Button
        GameObject purchaseButton = shopItemSlot.transform.Find("PurchaseButton").gameObject;
        purchaseButton.GetComponentInChildren<Text>().text = "Buy\n" + itemScriptableObject.ItemName + "\n$ " + itemScriptableObject.PurchaseCost;
        purchaseButton.GetComponent<PurchaseItemButton>().ItemObject = itemObject;
    }
}