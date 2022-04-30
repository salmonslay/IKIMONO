using System;
using System.Diagnostics;
using IKIMONO.UI;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace IKIMONO.Pet
{
    public class Test : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log($"Found {ItemScriptableObject.AllItems.Length} items in the database");
            Player.Instance.Pet.Hunger.Set(0);

            PetNeed.ValueUpdated += SetBars;
            UpdateAll();
        }

        public void PrintJson()
        {
            string json = Player.Instance.ToString();
            string path = System.IO.Path.GetTempPath();
            string fileName = $"{path}/{Player.Instance.Pet.Name}.json";
            System.IO.File.WriteAllText(fileName, json);
            Debug.Log($"Saved json to {fileName}");
            Debug.Log("Opening...");
            Process.Start(fileName);
        }

        public void RenamePet()
        {
            SetNamePanel.Open();
        }
        public void NukeData()
        {
            Player.Reset();
            Debug.Log("boom. restart the game");
        }

        public void UpdateAll()
        {
            Player.Instance.Pet.UpdateValues();
        }

        public void HowLongToZeroHunger()
        {
            Pet p = Player.Instance.Pet;
            Debug.Log($"{p.Name} will starve at {p.Hunger.GetTimeAtValue(0)}. Well not really, but the hunger bar will be at 0.");
        }

        public static void SetBars()
        {
            GameObject[] bars = GameObject.FindGameObjectsWithTag("DebugNeedBar");
            for(int i = 0; i < bars.Length; i++)
            {
                PetNeed need = Player.Instance.Pet.Needs[i];
                bars[i].GetComponent<Slider>().value = need.Percentage;
                bars[i].GetComponentInChildren<Text>().text =
                    $"{need.Name}: {Math.Round(need.Percentage * 100, 2)}%";
            }
        }
    }
}