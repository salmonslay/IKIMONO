using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FeedDropLocation : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            // Hitta vilket item som dragit.
            Item _itemToRemove = eventData.pointerDrag.GetComponent<DragDrop>().GetItem();

            //Kontrollera att det är ett fooditem.
            if (!_itemToRemove.ItemScriptableObject.ItemType.Equals(Item.ItemType.Food))
            {
                return;
            }

            // Ta bort foodItem ur inventory.
            PlayerTestClass.Instance.GetInventory().RemoveItem(_itemToRemove, 1);

            // TODO Lägg till funktion som att mata osv här.
        }
    }

}
