using System;
using System.Collections;
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

        private PetNeed _petNeed;
        private PetNeedEnergy _petNeedEnergy;
        private Button _button;

        private Animator _animator;
        private bool _canPlayAgain = true;
        [SerializeField] private Color _upArrowColor = new Color(179, 255, 165);
        [SerializeField] private Color _downArrowColor = new Color(179, 255, 165);

        private void Awake()
        {
            PetNeed.ValueUpdated += UpdateValue;

            Player.Instance.Pet.UpdateValues();

            if (_showName)
                GetComponentInChildren<Text>().text = Player.Instance.Pet.Name;

            _button = GetComponent<Button>();

            _petNeedEnergy = Player.Instance.Pet.Energy;
        }

        private bool arrowActiveFromSleep;
        private void Update()
        {
            _button.interactable = _petNeed.UsageCondition;

            // Show positive arrow on Energy Button if sleeping;
            if (typeof(PetNeedEnergy) == _petNeed.GetType() && _petNeedEnergy.IsSleeping)
            {
                SetArrowState(true);
                ArrowIsPositive(true);
                arrowActiveFromSleep = true;
            }
            else if (arrowActiveFromSleep && !_petNeedEnergy.IsSleeping)
            {
                SetArrowState(false);
                arrowActiveFromSleep = false;
            }
        }

        private void OnDestroy()
        {
            PetNeed.ValueUpdated -= UpdateValue;
        }

        private float _lastShownValue;
        private void ShowArrowOnValueChanged(float newValue)
        {
            // Return if not Basic Need.
            if (_petNeed.GetType() == typeof(PetNeedOverall)) return;

            // If the game was started more than 10 seconds ago.
            if (Time.time > 10)
            {
                // If coming from minigame.
                if (Time.timeSinceLevelLoad < 5)
                {
                    // Fun goes up, rest goes down.
                    if (_petNeed.GetType() == typeof(PetNeedFun))
                    {
                        ShowArrow(true);
                    }
                    else
                    {
                        ShowArrow(false);

                    }
                }
                else
                {

                    // Calculate change since last shown.
                    float valueChangeDelta = newValue - _lastShownValue;

                    // Return if change is too small.
                    // @TODO Set float to preferred update value after testing.
                    if (Math.Abs(valueChangeDelta) < 0.0001f) return;

                    // Show arrow.
                    ShowArrow(valueChangeDelta > 0);
                }
            }
            // If the game started less than 10 seconds ago, show all arrows down.
            // Energy arrow state is overriden in update if sleeping.
            else
            {
                ShowArrow(false);
            }

            // Always update last shown value after showing arrow.
            _lastShownValue = newValue;
        }


        /// <summary>
        /// Turns on or off arrow based on passed parameter; 
        /// </summary>
        /// <param name="isTurnedOn"></param>
        public void SetArrowState(bool isTurnedOn)
        {
            _arrow.enabled = isTurnedOn;
        }

        /// <summary>
        /// Shows positive/negative arrow over button once based on passed bool parameter.
        /// </summary>
        /// <param name="isPositive"></param>
        public void ShowArrow(bool isPositive)
        {
            _animator = _arrow.gameObject.GetComponent<Animator>();

            if (_canPlayAgain)
            {
                ArrowIsPositive(isPositive);
                StartCoroutine(ShowArrowEnumerator());
            }
        }

        public void ArrowIsPositive(bool isPositive)
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

        IEnumerator ShowArrowEnumerator()
        {
            _canPlayAgain = false;
            _arrow.enabled = true;
            _animator.Play("petNeed_arrow", 0, 0);
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
            _arrow.enabled = false;
            _canPlayAgain = true;
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