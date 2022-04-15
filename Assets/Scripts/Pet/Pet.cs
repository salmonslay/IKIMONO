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
        /// <summary>
        /// The name of the pet.
        /// </summary>
        [JsonProperty("name")] public string Name { get; }
        
        [JsonProperty("hunger")] public PetNeed Hunger { get; }

        [JsonProperty("social")] public PetNeed Social { get; }
        
        [JsonProperty("energy")] public PetNeed Energy { get; }
        
        [JsonProperty("fun")] public PetNeed Fun { get; }
        
        [JsonProperty("hygiene")] public PetNeed Hygiene { get; }
        
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