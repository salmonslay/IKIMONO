using System;
using Newtonsoft.Json;
using UnityEngine;

namespace IKIMONO.Pet
{
    public class Test : MonoBehaviour
    {
        private void Start()
        {
            Player.Instance.AddCoins(5);
            Player.Instance.Pet.Hunger.Increase(5);
            
            print(Player.Instance.ToString());
            Player.Instance.Save();
        }
    }
}