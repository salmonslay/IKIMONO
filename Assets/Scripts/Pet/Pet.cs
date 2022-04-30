using System;
using IKIMONO.UI;
using Newtonsoft.Json;
using UnityEngine;

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
        [JsonProperty("name")] public string Name { get; private set; }

        [JsonProperty("hunger")] public PetNeedHunger Hunger { get; } = new PetNeedHunger();

        [JsonProperty("social")] public PetNeedSocial Social { get; } = new PetNeedSocial();
        
        [JsonProperty("energy")] public PetNeedEnergy Energy { get; } = new PetNeedEnergy();
        
        [JsonProperty("fun")] public PetNeedFun Fun { get; } = new PetNeedFun();
        
        [JsonProperty("hygiene")] public PetNeedHygiene Hygiene { get; } = new PetNeedHygiene();
        
        public PetNeed Overall { get; } = new PetNeedOverall();
        
        public PetNeed[] Needs => new[] { Hunger, Social, Energy, Fun, Hygiene, Overall };

        public Pet()
        {
            Name = NameSet.GetRandomName();
        }
        public Pet (string name)
        {
            Name = name;
        }

        public void SetName(string name)
        {
            Name = name;
            Debug.Log($"Pet name set to {name}");
            Player.Instance.Save();
        }
        
        public void UpdateValues()
        {
            Hunger.UpdateValue();
            Social.UpdateValue();
            Energy.UpdateValue();
            Fun.UpdateValue();
            Hygiene.UpdateValue();
            Player.Instance.Save();
        }
        
        /// <summary>
        /// Convert the pet to a JSON-string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public float GetGeneralMood()
        {
            PetNeed[] needs = { Hunger, Energy, Hygiene, Fun };
            
            float total = 0;
            foreach (PetNeed need in needs)
            {
                if(need.Percentage < 0.2f)
                {
                    total -= 1;
                }
                else if(need.Percentage > 0.4f)
                {
                    total += need.Percentage;
                }
            }
            
            return Mathf.Clamp(total / needs.Length, 0, 1);
        }
    }
}