using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CoinShop : MonoBehaviour
{
    [SerializeField] private List<ScriptableObjects.CoinItemScriptableObject> itemList;

    [SerializeField] private GameObject _shopItemSlot;

    private void Start()
    {
        foreach (ScriptableObjects.CoinItemScriptableObject itemObject in itemList)
        {
            GameObject shopItemSlot = Instantiate(_shopItemSlot, GameObject.Find("CashShopViewContent").transform);
            SetupItemSlot(itemObject, shopItemSlot);
        }
    }

    private void SetupItemSlot(ScriptableObjects.CoinItemScriptableObject coinObject, GameObject shopItemSlot)
    {
        ScriptableObjects.CoinItemScriptableObject coinScriptableObject = coinObject;
        //Setup Image
        shopItemSlot.transform.Find("ItemImageContainer").Find("ItemImage").GetComponent<Image>().sprite = coinScriptableObject.Sprite;

        //Setup Button
        GameObject purchaseButton = shopItemSlot.transform.Find("PurchaseButton").gameObject;
        purchaseButton.GetComponentInChildren<Text>().text = "Buy\n" + coinScriptableObject.ItemName + "\n$ " + coinScriptableObject.realMoneyCost;
        purchaseButton.GetComponent<Button>().interactable = false;
    }
}