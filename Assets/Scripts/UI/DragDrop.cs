using IKIMONO.Pet;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler
{
    private ItemSlot _itemSlot;
    private Image _imageOriginal;
    private Image _imageCopy;
    private GameObject _toolTip;
    private Image maskImage;

    private CanvasGroup _overlayCanvasGroup;
    private Canvas _canvas;

    [SerializeField] private GameObject _foodInfoPrefab;
    [SerializeField] private GameObject _infoRow;

    [SerializeField] private Sprite _funSprite;
    [SerializeField] private Sprite _hungerSprite;
    [SerializeField] private Sprite _hygieneSprite;
    [SerializeField] private Sprite _energySprite;

    private void Awake()
    {
        _imageOriginal = transform.Find("Image").GetComponent<Image>();
        _itemSlot = GetComponent<ItemSlot>();
        _canvas = transform.GetComponentInParent<Canvas>();
        _overlayCanvasGroup = gameObject.GetComponentInParent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _imageCopy = CreateNewImageCopy();
        _toolTip = CreateFoodInfoTooltip();
        SetMaskColor(eventData.pointerCurrentRaycast.gameObject == GameObject.Find("Ikimono"));
    }

    private Image CreateNewImageCopy()
    {


        // Create outline.
        Image imageOutline = Instantiate(_imageOriginal, transform);
        Destroy(imageOutline.gameObject.GetComponent<CanvasGroup>());
        imageOutline.raycastTarget = false;
        imageOutline.transform.SetParent(_canvas.transform);
        imageOutline.transform.localScale = new Vector3(1.2f, 1.2f, 1);

        Color outlineColor = imageOutline.color;
        outlineColor.a = .5f;
        imageOutline.color = outlineColor;

        Mask mask = imageOutline.gameObject.AddComponent<Mask>();
        mask.showMaskGraphic = false;

        RectTransform outlineRect = imageOutline.rectTransform;
        // Create mask.
        GameObject maskColorImage = new GameObject("MaskColor");
        maskImage = maskColorImage.AddComponent<Image>();
        maskColorImage.GetComponent<Image>().raycastTarget = false;
        maskColorImage.transform.SetParent(imageOutline.transform);
        maskColorImage.transform.localScale = new Vector2(1, 1);
        maskColorImage.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        maskColorImage.GetComponent<RectTransform>().sizeDelta = new Vector2(135, 135);

        // Create item image.

        Image itemImage = Instantiate(_imageOriginal, transform);
        Destroy(itemImage.gameObject.GetComponent<CanvasGroup>());
        itemImage.raycastTarget = false;
        itemImage.transform.SetParent(maskColorImage.transform);
        itemImage.transform.localScale = new Vector3(.9f, .9f, 1);
        itemImage.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

        return imageOutline;
    }

    private GameObject CreateFoodInfoTooltip()
    {
        Vector3 offset = new Vector3(_imageCopy.GetComponent<RectTransform>().rect.width + 50, 0, 0);
        GameObject toolTip = Instantiate(_foodInfoPrefab, _imageCopy.transform.position, Quaternion.identity, _imageCopy.transform);
        toolTip.GetComponent<RectTransform>().localPosition += offset;

        ScriptableObjects.ItemScriptableObject itemScriptableObject = GetItem().ItemObject;

        if (itemScriptableObject.GetType() == typeof(FoodItemScriptableObject))
        {
            FoodItemScriptableObject foodItemScriptableObject = (FoodItemScriptableObject)itemScriptableObject;
            // Set Up Values.
            // Fun.
            GameObject itemInfoRow = Instantiate(_infoRow, toolTip.transform);
            Image image = itemInfoRow.GetComponentInChildren<Image>();
            image.sprite = _funSprite;
            image.maskable = false;
            itemInfoRow.GetComponentInChildren<Text>().maskable = false;
            itemInfoRow.GetComponentInChildren<Text>().text = (foodItemScriptableObject.FunValue >= 0 ? "+ " : "- ")
                + Math.Abs(foodItemScriptableObject.FunValue);
            // Hunger.
            itemInfoRow = Instantiate(_infoRow, toolTip.transform);
            image = itemInfoRow.GetComponentInChildren<Image>();
            image.sprite = _hungerSprite;
            image.maskable = false;
            itemInfoRow.GetComponentInChildren<Text>().maskable = false;
            itemInfoRow.GetComponentInChildren<Text>().text = (foodItemScriptableObject.HungerValue >= 0 ? "+ " : "- ")
                + Math.Abs(foodItemScriptableObject.HungerValue);
            //Hygiene
            itemInfoRow = Instantiate(_infoRow, toolTip.transform);
            image = itemInfoRow.GetComponentInChildren<Image>();
            image.sprite = _hygieneSprite;
            image.maskable = false;
            itemInfoRow.GetComponentInChildren<Text>().maskable = false;
            itemInfoRow.GetComponentInChildren<Text>().text = (foodItemScriptableObject.HygieneValue >= 0 ? "+ " : "- ")
                + Math.Abs(foodItemScriptableObject.HygieneValue);
            //Energy
            itemInfoRow = Instantiate(_infoRow, toolTip.transform);
            image = itemInfoRow.GetComponentInChildren<Image>();
            image.sprite = _energySprite;
            image.maskable = false;
            itemInfoRow.GetComponentInChildren<Text>().maskable = false;
            itemInfoRow.GetComponentInChildren<Text>().text = (foodItemScriptableObject.EnergyValue >= 0 ? "+ " : "- ")
                + Math.Abs(foodItemScriptableObject.EnergyValue);
        }

        return toolTip;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move Image copy on drag.
        _imageCopy.rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;

        SetMaskColor(eventData.pointerCurrentRaycast.gameObject == GameObject.Find("Ikimono"));
    }

    private void SetMaskColor(bool isOverPet)
    {
        Color outlineColor;
        if (isOverPet)
        {
            outlineColor = new Color(0, 255, 0, 127);
        }
        else
        {
            outlineColor = new Color(255, 0, 0, 127);
        }
        outlineColor.a = 0.3f;
        maskImage.color = outlineColor;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Set parent back to original so the reference to the item can be grabbed.
        _imageCopy.transform.SetParent(_itemSlot.transform);

        // Ta bort kopian som dragits runt.
        Destroy(_imageCopy.gameObject);

        // Reset canvasGroup values.
        _overlayCanvasGroup.alpha = 1;
        _overlayCanvasGroup.blocksRaycasts = true;
    }

    public Item GetItem()
    {
        // Find reference to item.
        return _itemSlot.Item;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Make Inventory slightly transparent and remove raycast blocking.
        _overlayCanvasGroup.alpha = 0f;
        _overlayCanvasGroup.blocksRaycasts = false;
    }
}
