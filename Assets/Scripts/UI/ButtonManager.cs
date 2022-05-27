using System;
using IKIMONO.Pet;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace IKIMONO.UI
{

    public class ButtonManager : MonoBehaviour
    {
        [SerializeField] private GameObject _uiInventoryWindow;
        [SerializeField] private GameObject _uiShopWindow;
        [SerializeField] private GameObject _uiSettingsWindow;

        private GameObject _activeWindow;



        public void OpenSettings()
        {
            ResetUIWindows();
            if (_activeWindow != _uiSettingsWindow)
            {
                _uiSettingsWindow.SetActive(true);
                _activeWindow = _uiSettingsWindow;
            }
            else
            {
                _activeWindow = null;
            }
        }

        public void OpenShop()
        {
            ResetUIWindows();
            if (_activeWindow != _uiShopWindow)
            {
                _uiShopWindow.SetActive(true);
                _activeWindow = _uiShopWindow;
            }
            else
            {
                _activeWindow = null;
            }
        }

        public void NeedFun()
        {
            if (!Player.Instance.MinigameJumpTutorialDone)
            {
                Player.Instance.MinigameJumpTutorialDone = true;
                SceneManager.LoadScene("MinigameTutorial");
            }
            else
            {
                SceneManager.LoadScene("MinigameJump");
            }


            AudioManager.Instance.StopSound("DayAmb", "One");
            AudioManager.Instance.StopSound("NightAmb", "One");
            AudioManager.Instance.PlaySound("MinigameMusic", "One");
            AudioManager.Instance.PlaySound("GameAmb", "One");
        }

        public void NeedHunger()
        {
            ResetUIWindows();
            if (_activeWindow != _uiInventoryWindow)
            {
                _uiInventoryWindow.SetActive(true);
                _activeWindow = _uiInventoryWindow;
                _uiInventoryWindow.GetComponentInParent<VerticalLayoutGroup>().padding.bottom = 1300;
            }
            else
            {
                _activeWindow = null;
            }
        }

        public void NeedEnergy()
        {


            //Player.Instance.Pet.SetIkimonoState(State.IsSleeping);
            PetNeedEnergy energy = Player.Instance.Pet.Energy;
            energy.IsSleeping = !energy.IsSleeping;
            Player.Instance.Save();
            FindObjectOfType<Ikimono>().SetSprite();
            Player.Instance.Pet.Hygiene.IsCleaning = false;

            if (energy.IsSleeping == true)
            {

                AudioManager.Instance.PlaySound("SleepMusic", "One");
            }
            else
            {
                AudioManager.Instance.PlaySound("Music", "One");
            }

        }



        public void NeedHygiene()
        {
            Player.Instance.Pet.Hygiene.IsCleaning = !Player.Instance.Pet.Hygiene.IsCleaning;
        }

        public void ResetUIWindows()
        {
            _uiInventoryWindow.SetActive(false);
            _uiShopWindow.SetActive(false);
            _uiSettingsWindow.SetActive(false);
            _uiInventoryWindow.GetComponentInParent<VerticalLayoutGroup>().padding.bottom = 850;
        }
        public void CloseAllUIWindows()
        {
            _uiInventoryWindow.SetActive(false);
            _uiShopWindow.SetActive(false);
            _uiSettingsWindow.SetActive(false);
            _activeWindow = null;
        }

        public void DeleteSave()
        {
            ResetBox.Open();
        }

        public static void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }

        public void ClickSounds()
        {
            AudioManager.Instance.PlaySound("Button", "One");


        }

    }
}