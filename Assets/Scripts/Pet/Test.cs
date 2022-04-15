using System;
using Newtonsoft.Json;
using UnityEngine;

namespace IKIMONO.Pet
{
    public class Test : MonoBehaviour
    {
        PetNeed petNeed = new PetNeedHunger();

        private void Start()
        {
            petNeed.Update();
            Debug.Log(JsonConvert.SerializeObject(petNeed));
        }
    }
}