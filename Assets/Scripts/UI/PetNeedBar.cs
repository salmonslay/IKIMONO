using System;
using IKIMONO.Pet;
using UnityEngine;
using UnityEngine.UI;

namespace IKIMONO.UI
{
    public class PetNeedBar : MonoBehaviour
    {
        [Tooltip("The image to fill with the pet's need.")]
        [SerializeField] private Image _fillImage;

        [Tooltip("The gradient to use for the fill image.")]
        [SerializeField] private Gradient _gradient;

        [Tooltip("Whether or not the pet's name should be displayed on the bar.")]
        [SerializeField] private bool _showName;

        [SerializeField] private Image _arrow;

        private Text _text;
        private PetNeed _petNeed;
        private PetNeedEnergy _petNeedEnergy;
        private Button _button;

        private Animator _animator;
        private bool _arrowActiveFromSleep;
        private float _lastShownValue;
        [SerializeField] private Color _upArrowColor = new Color(179, 255, 165);
        [SerializeField] private Color _downArrowColor = new Color(179, 255, 165);

        private bool _comingFromMiniGame = false;
        private bool _arrowIsPositive;
        private float _arrowTimer = 0;

        private void Awake()
        {
            if (Time.time > 5)
            {
                _comingFromMiniGame = true;
            }

            PetNeed.ValueUpdated += UpdateValue;

            Player.Instance.Pet.UpdateValues();

            _text = GetComponentInChildren<Text>();

            _button = GetComponent<Button>();

            _petNeedEnergy = Player.Instance.Pet.Energy;
            _animator = _arrow.gameObject.GetComponent<Animator>();
        }

        private void Update()
        {
            _button.interactable = _petNeed.UsageCondition && !Tutorial.IsTutorial;

            if (!Tutorial.IsTutorial && !_button.interactable)
            {
                if (Player.Instance.Pet.Energy.IsSleeping)
                {
                    _text.text = "Sleeping!";
                }
                else if (Player.Instance.Pet.Energy.Value < 20)
                {
                    _text.text = "Too tired!";
                } 
                else if (Player.Instance.Pet.Hunger.Value < 20)
                {
                    _text.text = "Too hungry!";
                }
            }
            else
            {
                _text.text = "";
            }


            if (_showName)
                _text.text = Player.Instance.Pet.Name;

            if (_arrowTimer >= 0)
            {
                _arrow.enabled = true;
                _arrowTimer -= Time.deltaTime;
            }
            else
            {
                _arrow.enabled = false;
            }

            // Show positive arrow on Energy Button if sleeping;
            if (typeof(PetNeedEnergy) == _petNeed.GetType() && _petNeedEnergy.IsSleeping)
            {
                ToggleArrowOn(true);
                SetArrowPositive(true);
                _arrowActiveFromSleep = true;
            }
            else if (_arrowActiveFromSleep && !_petNeedEnergy.IsSleeping)
            {
                ToggleArrowOn(false);
                _arrowActiveFromSleep = false;
            }
        }

        private void OnDestroy()
        {
            PetNeed.ValueUpdated -= UpdateValue;
        }
        
        private void ShowArrowOnValueChanged(float newValue)
        {
            if (_petNeed.GetType() == typeof(PetNeedOverall)) return;

            // Calculate change since last shown.
            float valueChangeDelta = newValue - _lastShownValue;

            if (Math.Abs(valueChangeDelta) < 0.0001f) return;

            // If the game started less than 2 seconds ago, show all arrows down.
            // Energy arrow state is overriden in update if sleeping.
            if (Time.time < 2)
            {
                // Return if not Basic Need.
                SetArrowPositive(false);
            }
            else
            {
                SetArrowPositive(valueChangeDelta > 0);

                if (!_comingFromMiniGame)
                {
                    if (_arrowIsPositive != (valueChangeDelta > 0))
                    {
                        ResetArrowAnimation();
                        SetArrowPositive(valueChangeDelta > 0);
                    }
                }
                else
                {
                    if (_petNeed.GetType() == typeof(PetNeedFun))
                    {
                        SetArrowPositive(true);
                    }
                    else
                    {
                        SetArrowPositive(false);
                    }
                    _comingFromMiniGame = false;
                }
            }
            // Always update last shown value after showing arrow.

            ShowArrowEnumerator();
            _lastShownValue = newValue;
            _arrowIsPositive = valueChangeDelta > 0;
        }

        /// <summary>
        /// Turns on or off arrow based on passed parameter; 
        /// </summary>
        /// <param name="isTurnedOn"></param>
        public void ToggleArrowOn(bool isTurnedOn)
        {
            _arrow.enabled = isTurnedOn;
        }

        public void SetArrowPositive(bool isPositive)
        {
            if (isPositive)
            {
                _arrow.gameObject.transform.localScale = new Vector3(1, 1, 1);
                _arrow.color = _upArrowColor;
            }
            else
            {
                _arrow.gameObject.transform.localScale = new Vector3(1, -1, 1);
                _arrow.color = _downArrowColor;
            }
        }

        private void ShowArrowEnumerator()
        {
            _animator = _arrow.gameObject.GetComponent<Animator>();
            _arrowTimer = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        }

        private void ResetArrowAnimation()
        {
            _animator.StopPlayback();
            _animator.Play("petNeed_arrow", 0, 0);
        }

        public void SetNeed(PetNeed need)
        {
            _petNeed = need;
        }
        
        private void UpdateValue()
        {
            if (_petNeed == null) return;

            float newValue = _petNeed.Percentage + 0.05f;

            ShowArrowOnValueChanged(newValue);

            _fillImage.fillAmount = newValue; // make sure the bar is always visible, even if it's at 0%
            _fillImage.color = _gradient.Evaluate(_petNeed.Percentage);
        }
    }
}