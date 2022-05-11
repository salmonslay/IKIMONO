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

        private void Awake()
        {
            PetNeed.ValueUpdated += UpdateValue;
            Player.Instance.Pet.UpdateValues();

            if (_showName)
                GetComponentInChildren<Text>().text = Player.Instance.Pet.Name;

            _button = GetComponent<Button>();

            _petNeedEnergy = Player.Instance.Pet.Energy;
        }

        private void Start()
        {
            if (typeof(PetNeedEnergy) == _petNeed.GetType())
            {
                SetArrowState(_petNeedEnergy.IsSleeping);
            }
        }

        private void Update()
        {
            _button.interactable = _petNeed.UsageCondition;
        }

        /// <summary>
        /// Turns on or off arrow based on passed parameter; 
        /// </summary>
        /// <param name="isTurnedOn"></param>
        public void SetArrowState(bool isTurnedOn)
        {
            _arrow.enabled = isTurnedOn;
        }

        private Animator _animator;
        private bool _canPlayAgain = true;
        [SerializeField] private Color _upArrowColor = new Color(179, 255, 165);
        [SerializeField] private Color _downArrowColor = new Color(179, 255, 165);
        /// <summary>
        /// Shows positive/negative arrow over button once based on passed bool parameter.
        /// </summary>
        /// <param name="isPositive"></param>
        public void ShowArrow(bool isPositive)
        {
            _animator = _arrow.gameObject.GetComponent<Animator>();

            if (_canPlayAgain)
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
                StartCoroutine(ShowArrowEnumerator());
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

        private void OnDestroy()
        {
            PetNeed.ValueUpdated -= UpdateValue;
        }

        private void UpdateValue()
        {
            if (_petNeed == null) return;

            _fillImage.fillAmount = _petNeed.Percentage + 0.05f; // make sure the bar is always visible, even if it's at 0%
            _fillImage.color = _gradient.Evaluate(_petNeed.Percentage);
        }
    }
}