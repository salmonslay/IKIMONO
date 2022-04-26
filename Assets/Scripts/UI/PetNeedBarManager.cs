using System;
using IKIMONO.Pet;
using UnityEngine;

namespace IKIMONO.UI
{
    public class PetNeedBarManager : MonoBehaviour
    {
        [SerializeField] private PetNeedBar _overallBar;
        [SerializeField] private PetNeedBar _funBar;
        [SerializeField] private PetNeedBar _hungerBar;
        [SerializeField] private PetNeedBar _hygieneBar;
        [SerializeField] private PetNeedBar _energyBar;
        private Pet.Pet _pet;

        private void Awake()
        {
            _pet = Player.Instance.Pet;
            SetNeeds();
        }

        public void SetNeeds()
        {
            _overallBar.SetNeed(_pet.Overall);
            _funBar.SetNeed(_pet.Fun);
            _hungerBar.SetNeed(_pet.Hunger);
            _hygieneBar.SetNeed(_pet.Hygiene);
            _energyBar.SetNeed(_pet.Energy);
        }
    }
}