using System;
using Newtonsoft.Json;
using UnityEngine;

namespace IKIMONO.Pet
{
    public class Test : MonoBehaviour
    {
        private void Start()
        {
            SaveFile saveFile = new SaveFile();
            print(saveFile.ToString());
        }
    }
}