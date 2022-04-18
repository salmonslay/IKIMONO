using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private ItemSlot _itemSlot;
    private Image _imageOriginal;
    private Image _imageCopy;

    [SerializeField] private Canvas _canvas;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _imageOriginal = transform.Find("Image").GetComponent<Image>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _itemSlot = GetComponent<ItemSlot>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Skapa kopia itembilden som ska drag runt.
        _imageCopy = Instantiate(_imageOriginal, transform);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Se till att bilden inte blockar raycasts s� att det g�r att droppa p� annat.
        _canvasGroup.blocksRaycasts = false;

        // S�tt l�tt transparens p� bilden som dras.
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
        // Blocka raycasts igen s� att det g�r att dra item igen.
        _canvasGroup.blocksRaycasts = true;
    }



    public Item GetItem()
    {
        return _itemSlot.GetItem();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Ta bort kopian som dragits runt.
        Destroy(_imageCopy.gameObject);
    }
}
