using Newtonsoft.Json;
using System;
using UnityEngine;

namespace IKIMONO.Pet
{

    public class PetNeedHunger : PetNeed
    {
        public override string Name => "Hunger";
        public override string NotificationTitle => "Your pet is hungry!";
        public override string NotificationDescription => "Your pet is hungry and needs to eat, come and feed it!";
        public override string NotificationIcon => "icon_hunger";
        public override bool UsageCondition => !Player.Instance.Pet.Energy.IsSleeping;
        public override float DecayRate => 2; // 50h to reach 0
    }

    public class PetNeedSocial : PetNeed
    {
        public override string Name => "Social";
        public override string NotificationTitle => "Your pet is lonely!";
        public override string NotificationDescription => "Your pet is lonely and needs to be socialized, come and play with it!";
        public override string NotificationIcon => "icon_social";
        public override bool UsageCondition => true;
        public override float DecayRate => 0f; // Will not decay, not used for now
        public override bool HasNotifications => false;
    }

    public class PetNeedEnergy : PetNeed
    {
        public override string Name => "Energy";
        public override string NotificationTitle => "Your pet is tired!";
        public override string NotificationDescription => "Your pet is tired and needs to sleep, come and give it a nap!";
        public override string NotificationIcon => "icon_energy";
        public override bool UsageCondition => true;
        public override float DecayRate => IsSleeping ? -12 : 5; // 20h to reach 0, 8h to reach 100
        [JsonProperty("sleeping")] public bool IsSleeping { get; set; }
    }

    public class PetNeedFun : PetNeed
    {
        public override string Name => "Fun";
        public override string NotificationTitle => "Your pet is bored!";
        public override string NotificationDescription => "Your pet is bored and needs to be played with, come and play with it!";
        public override string NotificationIcon => "icon_fun";

        public override bool UsageCondition => !Player.Instance.Pet.Energy.IsSleeping &&
                                               Player.Instance.Pet.Energy.Value > 20; // Hunger can not be a condition right now, as games are needed to feed the pet
        public override float DecayRate => 1.2f; // 83h to reach 0

        public const float PetAmountMax = 15;
        [JsonProperty("pettedAmount")] private float _pettedAmount = 0;
        [JsonProperty("pettedUpdatedAt")] public DateTime PettedUpdateTime { get; private set; } = DateTime.Now;
        public float PettedAmount
        {
            get => _pettedAmount;
            set
            {
                if (value > 0)
                {
                    _pettedAmount = Mathf.Min(_pettedAmount + value, PetAmountMax);
                }
                else
                {
                    _pettedAmount = Mathf.Max(_pettedAmount + value, 0);
                }
                PettedUpdateTime = DateTime.Now;
                //Debug.Log(_pettedAmount);
            }
        }
    }

    public class PetNeedHygiene : PetNeed
    {
        public override string Name => "Hygiene";
        public override string NotificationTitle => "Your pet is dirty!";
        public override string NotificationDescription => "Your pet is dirty and needs to be cleaned, come and clean it!";
        public override string NotificationIcon => "icon_hygiene";
        public override bool UsageCondition => !Player.Instance.Pet.Energy.IsSleeping &&
                                               Player.Instance.Pet.Energy.Value > 20;
        public override float DecayRate => 0.8f; // 125h to reach 0

        public event EventHandler OnCleaningStateChanged;

        private bool _isCleaning = false;
        public bool IsCleaning
        {
            get => _isCleaning;
            set
            {
                _isCleaning = value;
                OnCleaningStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public class PetNeedOverall : PetNeed
    {
        public override string Name { get; } = "Overall";
        public override string NotificationTitle { get; }
        public override string NotificationDescription { get; }
        public override string NotificationIcon { get; }
        public override bool UsageCondition => true;
        public override bool HasNotifications => false;
        public override float Percentage => Player.Instance.Pet.GetGeneralMood();

        // TODO: Value always returns 100, need to calculate it
    }

}
