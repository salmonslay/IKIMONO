using System;
using IKIMONO.Pet;
using UnityEngine;
using UnityEngine.UI;

namespace IKIMONO.UI
{
    public class PetNeedBar : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private Gradient _gradient;
        private PetNeed _petNeed;

        private void Awake()
        {
            PetNeed.ValueUpdated += UpdateValue;
            Player.Instance.Pet.UpdateValues();
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