using IKIMONO.Pet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    private ItemSlot _itemSlot;
    private Image _imageOriginal;
    private Image _imageCopy;

    private Canvas _canvas;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _imageOriginal = transform.Find("Image").GetComponent<Image>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _itemSlot = GetComponent<ItemSlot>();
        _canvas = transform.GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Skapa kopia itembilden som ska drag runt.
        _imageCopy = Instantiate(_imageOriginal, transform);
        _imageCopy.transform.SetParent(_canvas.transform);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Se till att bilden inte blockar raycasts så att det går att droppa på annat.
        _canvasGroup.blocksRaycasts = false;

        // Sätt lätt transparens på bilden som dras.
        Color _imageCopyColor = _imageCopy.color;
        _imageCopyColor.a = 0.6f;
        _imageCopy.color = _imageCopyColor;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Flytta kopian av bilden ondrag.
        _imageCopy.rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Blocka raycasts igen så att det går att dra item igen.
        _canvasGroup.blocksRaycasts = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _imageCopy.transform.SetParent(_itemSlot.transform);
        // Ta bort kopian som dragits runt.
        Destroy(_imageCopy.gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Player.Instance.Inventory.RemoveItem(GetItem(), 1);
    }

    public Item GetItem()
    {
        return _itemSlot.GetItem();
    }

}
