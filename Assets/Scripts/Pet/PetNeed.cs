using System;
using Newtonsoft.Json;
using UnityEngine;

namespace IKIMONO.Pet
{
    /// <summary>
    /// Base class for all pet needs.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class PetNeed
    {
        
        public delegate void ValueUpdatedEventHandler();
        
        /// <summary>
        /// Called when the pet's values are updated.
        /// </summary>
        public static event ValueUpdatedEventHandler ValueUpdated;
        
        public abstract string Name { get; }
        public virtual float MaxValue { get; } = 100;
        public virtual float MinValue { get; } = 0;
        public abstract string NotificationTitle { get; }
        public abstract string NotificationDescription { get; }
        public abstract string NotificationIcon { get; }
        
        /// <summary>
        /// How much the need will decrease per hour. MaxValue/DecayRate = hours to reach MinValue.
        /// </summary>
        public virtual float DecayRate { get; } = 5;

        [JsonProperty("value")] private float _value = 100;

        /// <summary>
        /// The current value of the need.
        /// </summary>
        public float Value
        {
            get => _value;
            private set => _value = Mathf.Clamp(value, MinValue, MaxValue); // clamp to min/max values
        }

        /// <summary>
        /// The current percentage of the need (0.0-1.0).
        /// </summary>
        public float Percentage => (Value - MinValue) / (MaxValue - MinValue);

        /// <summary>
        /// The last time the need was updated.
        /// </summary>
        [JsonProperty("updatedAt")]
        public DateTime LastUpdated { get; private set; } = DateTime.Now;
        
        public DateTime TimeAtMinValue => LastUpdated + TimeSpan.FromHours(MaxValue / DecayRate);

        /// <summary>
        /// Get the time at which the need will reach a specific value.
        /// </summary>
        public DateTime GetTimeAtValue(float value)
        {
            /*
             * Formula:
             * mv = 100, dr = 2
             * v = 50, ex = 25
             * v / dr = 25
             */
            UpdateValue();
            TimeSpan span = TimeSpan.FromHours(value / DecayRate);
            Debug.Log(this);
            Debug.Log(value);
            Debug.Log(DecayRate);
            Debug.Log(value / DecayRate);
            return LastUpdated + span;
        }

        /// <summary>
        /// Update this need to a new value from time.
        /// </summary>
        public void UpdateValue()
        {
            float oldValue = Value;
            // calculate delta 
            DateTime now = DateTime.Now;
            TimeSpan elapsed = now - LastUpdated;
            float delta = (float)elapsed.TotalHours * DecayRate;
            
            // update value
            Value = Math.Max(MinValue, Math.Min(MaxValue, Value - delta));
            LastUpdated = now;
            
            ValueUpdated?.Invoke();

            Debug.Log($"{Name} updated after {Math.Round(elapsed.TotalMinutes, 2)} minutes, from {oldValue} to {Value}. Delta: {delta}");
        }
        
        /// <summary>
        /// Increase the value of this need by a certain amount.
        /// </summary>
        /// <param name="amount">The amount to increase this value with</param>
        public void Increase(float amount)
        {
            UpdateValue();
            Value = Math.Min(MaxValue, Value + amount);
        }
        
        /// <summary>
        /// Decrease the value of this need by a certain amount.
        /// </summary>
        /// <param name="amount">The amount to decrease this value with</param>
        public void Decrease(float amount)
        {
            Value = Math.Max(MinValue, Value - amount);
        }

        public override string ToString()
        {
            return $"{Name}: {Value}/{MaxValue}";
        }
    }
}