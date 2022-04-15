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
        /// How much the need will decrease per hour
        /// </summary>
        public virtual int DecayRate { get; } = 5;

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
        public void Update()
        {
            // calculate delta 
            DateTime now = DateTime.Now;
            TimeSpan elapsed = now - LastUpdated;
            float delta = (float)elapsed.TotalHours * DecayRate;
            
            // update value
            Value = Math.Max(MinValue, Math.Min(MaxValue, Value - delta));
            LastUpdated = now;
            
            Debug.Log("Updated " + Name + " to " + Value);
        }
    }
}