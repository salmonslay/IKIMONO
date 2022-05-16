using IKIMONO.Pet;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler
{
    private ItemSlot _itemSlot;
    private Image _imageOriginal;
    private Image _imageCopy;

    private CanvasGroup _overlayCanvasGroup;

    private Canvas _canvas;

    private void Awake()
    {
        _imageOriginal = transform.Find("Image").GetComponent<Image>();
        _itemSlot = GetComponent<ItemSlot>();
        _canvas = transform.GetComponentInParent<Canvas>();
        _overlayCanvasGroup = gameObject.GetComponentInParent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Create a copy of Image, to drag.
        _imageCopy = Instantiate(_imageOriginal, transform);

        // Make sure that it doesn't block raycasts.
        _imageCopy.raycastTarget = false;

        // Set parent  to canvas so that it ignores canvasgroup changes.
        _imageCopy.transform.SetParent(_canvas.transform);

        // Set slight transparancy.
        Color _imageCopyColor = _imageCopy.color;
        _imageCopyColor.a = 0.6f;
        _imageCopy.color = _imageCopyColor;

        // Increase size of image a small amount.
        _imageCopy.transform.localScale = new Vector3(1.1f, 1.1f, 1);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move Image copy on drag.
        _imageCopy.rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
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
