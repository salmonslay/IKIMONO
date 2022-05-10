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
        
        private PetNeed _petNeed;

        private void Awake()
        {
            PetNeed.ValueUpdated += UpdateValue;
            Player.Instance.Pet.UpdateValues();

            if (_showName)
                GetComponentInChildren<Text>().text = Player.Instance.Pet.Name;
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