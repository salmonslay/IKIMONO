using IKIMONO.Pet;
using IKIMONO.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class PetInteraction : MonoBehaviour, IDropHandler, IPointerDownHandler, IDragHandler
{

    [SerializeField] private GameObject _spongePrefab;
    private GameObject _sponge;

    private Canvas _canvas;
    RectTransform _canvasRectTransform;
    private Vector2 _uiOffset;

    private PetNeedHygiene _petHygiene;

    private PetNeedBar _funButton;
    private PetNeedBar _hungerButton;
    private PetNeedBar _hygieneButton;
    private PetNeedBar _energyButton;

    private void Awake()
    {
        _canvas = transform.GetComponentInParent<Canvas>();
        _canvasRectTransform = _canvas.GetComponent<RectTransform>();

        _uiOffset = new Vector2((float)_canvasRectTransform.sizeDelta.x / 2, (float)_canvasRectTransform.sizeDelta.y / 2);
        _petHygiene = Player.Instance.Pet.Hygiene;
    }

    private void Start()
    {
        _petHygiene.OnCleaningStateChanged += OnCleaningStateChanged;
        _funButton = GameObject.Find("Need Fun").GetComponent<PetNeedBar>();
        _hungerButton = GameObject.Find("Need Hunger").GetComponent<PetNeedBar>();
        _hygieneButton = GameObject.Find("Need Hygiene").GetComponent<PetNeedBar>();
        _energyButton = GameObject.Find("Need Energy").GetComponent<PetNeedBar>();
    }

    private void OnCleaningStateChanged(object sender, System.EventArgs e)
    {
        if (_petHygiene.IsCleaning)
        {
            _sponge = Instantiate(_spongePrefab, transform.position, Quaternion.identity, transform);
        }
        else
        {
            Destroy(_sponge);
            _sponge = null;
        }
    }

    private void OnDestroy()
    {
        _petHygiene.OnCleaningStateChanged -= OnCleaningStateChanged;
    }

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

            PetNeed fun = Player.Instance.Pet.Fun;
            PetNeed hunger = Player.Instance.Pet.Hunger;
            PetNeed hygiene = Player.Instance.Pet.Hygiene;
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

                // Increase of decrease relevant PetNeed;
                // Play arrow animation on relevant button.
                // FunButton.
                if (food.FunValue > 0)
                {
                    _funButton.ShowArrow(true);
                    fun.Increase(food.FunValue);
                }
                else if (food.FunValue < 0)
                {
                    _funButton.ShowArrow(false);
                    fun.Decrease(food.FunValue);
                }
                // HungerButton.
                if (food.HungerValue > 0)
                {
                    _hungerButton.ShowArrow(true);
                    hunger.Increase(food.HungerValue);
                }
                else if (food.HungerValue < 0)
                {
                    _hungerButton.ShowArrow(false);
                    hunger.Decrease(food.HungerValue);
                }
                // HygieneButton.
                if (food.HygieneValue > 0)
                {
                    _hygieneButton.ShowArrow(true);
                    hygiene.Increase(food.HygieneValue);
                }
                else if (food.HygieneValue < 0)
                {
                    _hygieneButton.ShowArrow(false);
                    hygiene.Decrease(food.HygieneValue);
                }
                // EnergyButton.
                if (food.EnergyValue > 0)
                {
                    _energyButton.ShowArrow(true);
                    energy.Increase(food.EnergyValue);
                }
                else if (food.EnergyValue < 0)
                {
                    _energyButton.ShowArrow(false);
                    energy.Decrease(food.EnergyValue);
                }

            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_petHygiene.IsCleaning)
            _sponge.GetComponent<RectTransform>().anchoredPosition = eventData.pressPosition - _uiOffset;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != gameObject)
        {
            return;
        }
        if (!_petHygiene.IsCleaning)
        {
            PetIkimono(0.05f);
        }
        else if (_petHygiene.IsCleaning)
        {
            MoveSponge(eventData.delta / _canvas.scaleFactor);
            Clean(0.05f);

            // @PhilipAudio: Play the cleaning sound here.
            // Is run multiple times per second like the old script so a cooldown is still needed.
        }
    }

    private void Clean(float amount)
    {
        Player.Instance.Pet.Hygiene.Increase(amount);
    }

    private void MoveSponge(Vector2 moveDelta)
    {
        _sponge.GetComponent<RectTransform>().anchoredPosition += moveDelta;
    }

    private void PetIkimono(float amount)
    {
        Player.Instance.Pet.Fun.Increase(amount);
    }


}
