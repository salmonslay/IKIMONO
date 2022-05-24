using IKIMONO.Pet;
using IKIMONO.UI;
using System;
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
    private PetNeedFun _petFun;

    private PetNeedBar _funButton;
    private PetNeedBar _hungerButton;
    private PetNeedBar _hygieneButton;
    private PetNeedBar _energyButton;

    private bool _canPlaySound = true;
    private AudioSource _audioSource;

    private Pet _pet;
    private static readonly int Eat = Animator.StringToHash("Eat");
    private static readonly int Scratch = Animator.StringToHash("Scratch");


    private void Awake()
    {
        _canvas = transform.GetComponentInParent<Canvas>();
        _canvasRectTransform = _canvas.GetComponent<RectTransform>();


        _uiOffset = new Vector2((float)_canvasRectTransform.sizeDelta.x / 2, (float)_canvasRectTransform.sizeDelta.y / 2);
        _pet = Player.Instance.Pet;
        _petHygiene = _pet.Hygiene;
        _petFun = _pet.Fun;

    }

    private void Start()
    {
        _petHygiene.OnCleaningStateChanged += OnCleaningStateChanged;
        _funButton = GameObject.Find("Need Fun").GetComponent<PetNeedBar>();
        _hungerButton = GameObject.Find("Need Hunger").GetComponent<PetNeedBar>();
        _hygieneButton = GameObject.Find("Need Hygiene").GetComponent<PetNeedBar>();
        _energyButton = GameObject.Find("Need Energy").GetComponent<PetNeedBar>();
        _audioSource = AudioManager.Instance.effectsource;

        PetNeed.ValueUpdated += UpdatePettedValue;
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
        PetNeed.ValueUpdated -= UpdatePettedValue;

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
                    fun.Increase(food.FunValue);
                }
                else if (food.FunValue < 0)
                {
                    fun.Decrease(-food.FunValue);
                }
                // HungerButton.
                if (food.HungerValue > 0)
                {
                    hunger.Increase(food.HungerValue);
                }
                else if (food.HungerValue < 0)
                {
                    hunger.Decrease(-food.HungerValue);
                }
                // HygieneButton.
                if (food.HygieneValue > 0)
                {
                    hygiene.Increase(food.HygieneValue);
                }
                else if (food.HygieneValue < 0)
                {
                    hygiene.Decrease(-food.HygieneValue);
                }
                // EnergyButton.
                if (food.EnergyValue > 0)
                {
                    energy.Increase(food.EnergyValue);
                }
                else if (food.EnergyValue < 0)
                {
                    energy.Decrease(-food.EnergyValue);
                }

                AudioManager.Instance.RandomizeSound("Eating");
                Ikimono.Animator.SetTrigger(Eat);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_petHygiene.IsCleaning)
        {
            _sponge.GetComponent<RectTransform>().anchoredPosition = eventData.pressPosition - _uiOffset;
        }

        if (_pet.Energy.IsSleeping)
        {
            // namnamnamn sound

        }
        else if (_pet.Overall.Percentage < 0.3f)
        {
            AudioManager.Instance.RandomizeSound("Sad");    // Sad Sounds

        }
        else
        {
            AudioManager.Instance.RandomizeSound("Happy");  // Happy sounds
        }
    }



    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != gameObject)
        {
            return;
        }
        if (_pet.Energy.IsSleeping)
        {
            return;
        }
        if (_canPlaySound)
        {
            StartCoroutine(PlaySound());
        }

        if (!_petHygiene.IsCleaning)
        {
            PetIkimono(0.05f);
        }
        else
        {
            MoveSponge(eventData.delta / _canvas.scaleFactor);
            Clean(0.05f);
        }
        
        Ikimono.Animator.SetTrigger(Scratch);
    }

    IEnumerator PlaySound()
    {
        _canPlaySound = false;

        AudioClip clip;

        if (_pet.Hygiene.IsCleaning)
        {
            Debug.Log("cleansetsound");
            clip = _bubbleSounds[UnityEngine.Random.Range(0, _bubbleSounds.Length)];
        }
        else
        {
            Debug.Log("sratchnsetsound");
            clip = _scratchSounds[UnityEngine.Random.Range(0, _scratchSounds.Length)];

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
        if (_petFun.Percentage >= .99f || _petFun.PettedAmount >= PetNeedFun.PetAmountMax) return;

        _petFun.Increase(amount);
        _petFun.PettedAmount = amount;
    }


    public void UpdatePettedValue()
    {
        float oldValue = _petFun.PettedAmount;
        // calculate delta 
        DateTime now = DateTime.Now;
        TimeSpan elapsed = now - _petFun.PettedUpdateTime;
        float delta = (float)elapsed.TotalHours * _petFun.DecayRate / 2;

        // update value
        _petFun.PettedAmount = -delta;
    }
}
