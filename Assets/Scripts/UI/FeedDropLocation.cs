using IKIMONO.Pet;
using UnityEngine;
using UnityEngine.EventSystems;

public class FeedDropLocation : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DragDrop>() != null)
        {
            // Find what Item Object has been dragged.
            Item item = eventData.pointerDrag.GetComponent<DragDrop>().GetItem();

            // Return if itemtype is not food.
            if (item.ItemObject.GetType() != typeof(FoodItemScriptableObject))
            {
                return;
            }

            FoodItemScriptableObject food = (FoodItemScriptableObject)item.ItemObject;

            PetNeed hunger = Player.Instance.Pet.Hunger;
            PetNeed energy = Player.Instance.Pet.Energy;

            // TODO: edge-case for energy items?
            if (food.EnergyValue < 1 && energy.Value > energy.MaxValue - 1 && hunger.Value > hunger.MaxValue - 1)
            {
                // TODO: gör nåt åt det?
                return;
            }
            else
            {
                // Ta bort foodItem ur inventory.
                Player.Instance.Inventory.RemoveItem(item, 1);
                // Add food to hunger
                hunger.Increase(food.HungerValue);
                // Add food to energy
                energy.Increase(food.EnergyValue);
            }
        }
    }

}
