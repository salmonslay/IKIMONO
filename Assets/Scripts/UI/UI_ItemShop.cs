using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemShop : MonoBehaviour
{
    [SerializeField] private List<ScriptableObjects.ItemScriptableObject> itemList;

    [SerializeField] private GameObject _shopItemSlot;
    [SerializeField] private GameObject _infoRow;

    [SerializeField] private Sprite _funSprite;
    [SerializeField] private Sprite _hungerSprite;
    [SerializeField] private Sprite _hygieneSprite;
    [SerializeField] private Sprite _energySprite;

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
            // Set Up Values.
            // Fun.
            GameObject itemInfoRow = Instantiate(_infoRow, itemInfo.transform);
            itemInfoRow.GetComponentInChildren<Image>().sprite = _funSprite;
            itemInfoRow.GetComponentInChildren<Text>().text = (foodItemScriptableObject.FunValue >= 0 ? "+ " : "- ")
                + Math.Abs(foodItemScriptableObject.FunValue);
            // Hunger.
            itemInfoRow = Instantiate(_infoRow, itemInfo.transform);
            itemInfoRow.GetComponentInChildren<Image>().sprite = _hungerSprite;
            itemInfoRow.GetComponentInChildren<Text>().text = (foodItemScriptableObject.HungerValue >= 0 ? "+ " : "- ")
                + Math.Abs(foodItemScriptableObject.HungerValue);
            //Hygiene
            itemInfoRow = Instantiate(_infoRow, itemInfo.transform);
            itemInfoRow.GetComponentInChildren<Image>().sprite = _hygieneSprite;
            itemInfoRow.GetComponentInChildren<Text>().text = (foodItemScriptableObject.HygieneValue >= 0 ? "+ " : "- ")
                + Math.Abs(foodItemScriptableObject.HygieneValue);
            //Energy
            itemInfoRow = Instantiate(_infoRow, itemInfo.transform);
            itemInfoRow.GetComponentInChildren<Image>().sprite = _energySprite;
            itemInfoRow.GetComponentInChildren<Text>().text = (foodItemScriptableObject.EnergyValue >= 0 ? "+ " : "- ")
                + Math.Abs(foodItemScriptableObject.EnergyValue);
        }

        //Setup Button
        GameObject purchaseButton = shopItemSlot.transform.Find("PurchaseButton").gameObject;
        purchaseButton.GetComponentInChildren<Text>().text = "Buy\n" + itemScriptableObject.ItemName + "\n$ " + itemScriptableObject.PurchaseCost;
        purchaseButton.GetComponent<PurchaseItemButton>().ItemObject = itemObject;
    }
}