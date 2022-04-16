using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace IKIMONO.Pet
{
    /// <summary>
    /// A class that contains all the data for a single pet/Ikimono.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Pet
    {
        public delegate void UpdatedValuesEventHandler();
        
        /// <summary>
        /// Called when the pet's values are updated.
        /// </summary>
        public static event UpdatedValuesEventHandler UpdatedValues;
        
        /// <summary>
        /// The name of the pet.
        /// </summary>
        [JsonProperty("name")] public string Name { get; }

        [JsonProperty("hunger")] public PetNeed Hunger { get; } = new PetNeedHunger();

        [JsonProperty("social")] public PetNeed Social { get; } = new PetNeedSocial();
        
        [JsonProperty("energy")] public PetNeed Energy { get; } = new PetNeedEnergy();
        
        [JsonProperty("fun")] public PetNeed Fun { get; } = new PetNeedFun();
        
        [JsonProperty("hygiene")] public PetNeed Hygiene { get; } = new PetNeedHygiene();
        
        public PetNeed[] Needs => new[] { Hunger, Social, Energy, Fun, Hygiene };
        
        public Pet (string name)
        {
            Name = name;
        }
        
        public void UpdateValues()
        {
            Hunger.UpdateValue();
            Social.UpdateValue();
            Energy.UpdateValue();
            Fun.UpdateValue();
            Hygiene.UpdateValue();
            UpdatedValues?.Invoke();
        }
        
        /// <summary>
        /// Convert the pet to a JSON-string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}