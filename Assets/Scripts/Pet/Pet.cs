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
        public string Name { get; }
        
        public PetNeed Hunger { get; }

        public PetNeed Social { get; }
        
        public PetNeed Energy { get; }
        
        public PetNeed Fun { get; }
        
        public PetNeed Hygiene { get; }
    }
}