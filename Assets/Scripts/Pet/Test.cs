using System;
using IKIMONO.UI;
using UnityEngine;
using UnityEngine.UI;

namespace IKIMONO.Pet
{
    public class Test : MonoBehaviour
    {
        private void Start()
        {
            PetNeed.ValueUpdated += SetBars;
            UpdateAll();
        }

        public void RenamePet()
        {
            SetNamePanel.Open();
        }
        public void NukeData()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("boom. restart the game");
        }

        public void UpdateAll()
        {
            Player.Instance.Pet.UpdateValues();
        }

        public void HowLongToZeroHunger()
        {
            DateTime timeSpan = Player.Instance.Pet.Hunger.TimeAtMinValue;
            Debug.Log("Time to zero hunger: " + timeSpan.ToString("HH:mm:ss"));
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