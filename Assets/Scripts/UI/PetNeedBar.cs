using System;
using IKIMONO.Pet;
using UnityEngine;
using UnityEngine.UI;

namespace IKIMONO.UI
{
    public class PetNeedBar : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        private PetNeed _petNeed;

        private void Awake()
        {
            PetNeed.ValueUpdated += UpdateValue;
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
            
            _fillImage.fillAmount = _petNeed.Percentage;
        }
    }
}