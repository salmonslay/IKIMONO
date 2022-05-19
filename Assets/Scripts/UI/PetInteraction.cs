using IKIMONO.Pet;
using IKIMONO.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PetInteraction : MonoBehaviour, IDropHandler, IPointerDownHandler, IDragHandler
{

    [SerializeField] private GameObject _spongePrefab;
    [SerializeField] private AudioClip[] _bubbleSounds;
    [SerializeField] private AudioClip[] _scratchSounds;

    private GameObject _sponge;

    private Canvas _canvas;
    RectTransform _canvasRectTransform;
    private Vector2 _uiOffset;

    private PetNeedHygiene _petHygiene;

    private PetNeedBar _funButton;
    private PetNeedBar _hungerButton;
    private PetNeedBar _hygieneButton;
    private PetNeedBar _energyButton;

    private bool _canPlaySound = true;
    private AudioSource _audioSource;

    private Pet _pet;

    private void Awake()
    {
        _canvas = transform.GetComponentInParent<Canvas>();
        _canvasRectTransform = _canvas.GetComponent<RectTransform>();
       

        _uiOffset = new Vector2((float)_canvasRectTransform.sizeDelta.x / 2, (float)_canvasRectTransform.sizeDelta.y / 2);
        _pet = Player.Instance.Pet;
        _petHygiene = _pet.Hygiene;

    }

    private void Start()
    {
        _petHygiene.OnCleaningStateChanged += OnCleaningStateChanged;
        _funButton = GameObject.Find("Need Fun").GetComponent<PetNeedBar>();
        _hungerButton = GameObject.Find("Need Hunger").GetComponent<PetNeedBar>();
        _hygieneButton = GameObject.Find("Need Hygiene").GetComponent<PetNeedBar>();
        _energyButton = GameObject.Find("Need Energy").GetComponent<PetNeedBar>();
        _audioSource = AudioManager.Instance.effectsource;
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
            _audioSource = null;
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

                //Om den inte får äta ljud
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
                // ljud när den har ätit något
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_petHygiene.IsCleaning) { 
            _sponge.GetComponent<RectTransform>().anchoredPosition = eventData.pressPosition - _uiOffset;
        }

        if (_pet.Energy.IsSleeping)
        {

        }
        else if(_pet.Overall.Percentage < 0.3f){
            AudioManager.Instance.RandomizeSound("Sad");
            //sad sounds;
        }
        else
        {
            AudioManager.Instance.RandomizeSound("Happy");
        }
        // ljud för klia
        // ljud för glad
    }

    

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != gameObject)
        {
            return;
        }
        if(_pet.Energy.IsSleeping){
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

        }

        if (_canPlaySound)
        {
            StartCoroutine(PlaySound());
        }
    }

    IEnumerator PlaySound()
    {
        _canPlaySound = false;

        AudioClip clip;

        if (_pet.Hygiene.IsCleaning)
        {
            clip = _bubbleSounds[Random.Range(0, _bubbleSounds.Length)];
        }
        else
        {
            clip = _scratchSounds[Random.Range(0, _scratchSounds.Length)];
            
        }
        
        _audioSource.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length * 0.5f);
        _canPlaySound = true;
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
