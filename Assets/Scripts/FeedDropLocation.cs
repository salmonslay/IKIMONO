using System.Collections;
using System.Collections.Generic;
using IKIMONO.Pet;
using UnityEngine;
using UnityEngine.EventSystems;

public class FeedDropLocation : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            // Hitta vilket item som dragit.
            Item item = eventData.pointerDrag.GetComponent<DragDrop>().GetItem();

            //Kontrollera att det �r ett fooditem.
            if (!item.ItemScriptableObject.ItemType.Equals(Item.ItemType.Food))
            {
                return;
            }

            // Ta bort foodItem ur inventory.
            PlayerTestClass.Instance.GetInventory().RemoveItem(item, 1);

            PetNeed hunger = Player.Instance.Pet.Hunger;
            if (hunger.Value > hunger.MaxValue - 1)
            {
                // TODO: gör nåt åt det?
                return;
            }
            else
            {
                hunger.Increase(item.ItemScriptableObject.FoodValue);
            }
        }
    }

}
