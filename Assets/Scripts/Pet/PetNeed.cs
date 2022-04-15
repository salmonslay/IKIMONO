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
        public abstract string Name { get; }
        public virtual float MaxValue { get; } = 100;
        public virtual float MinValue { get; } = 0;

        /// <summary>
        /// How much the need will decrease per hour. MaxValue/DecayRate = hours to reach MinValue.
        /// </summary>
        public virtual float DecayRate { get; } = 5;

        [JsonProperty("value")]
        private float _value;

        /// <summary>
        /// The current value of the need.
        /// </summary>
        public float Value
        {
            get => _value;
            private set => _value = Mathf.Clamp(value, MinValue, MaxValue); // clamp to min/max values
        }

        /// <summary>
        /// The current percentage of the need (0-100).
        /// </summary>
        public float Percentage => (Value - MinValue) / (MaxValue - MinValue);

        /// <summary>
        /// The last time the need was updated.
        /// </summary>
        [JsonProperty("updatedAt")]
        public DateTime LastUpdated { get; private set; }

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
    }
}