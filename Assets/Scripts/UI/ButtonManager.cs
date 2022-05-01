using System;
using IKIMONO.Pet;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IKIMONO.UI
{

    public class ButtonManager : MonoBehaviour
    {
        [SerializeField] private GameObject _uiInventoryWindow;
        [SerializeField] private GameObject _uiShopWindow;
        [SerializeField] private GameObject _uiSettingsWindow;

        private GameObject activeWindow;

        public void OpenSettings()
        {
            ResetUIWindows();
            if (activeWindow != _uiSettingsWindow)
            {
                _uiSettingsWindow.SetActive(true);
                activeWindow = _uiSettingsWindow;
            }
            else
            {
                activeWindow = null;
            }
        }

        public void OpenShop()
        {
            ResetUIWindows();
            if (activeWindow != _uiShopWindow)
            {
                _uiShopWindow.SetActive(true);
                activeWindow = _uiShopWindow;
            }
            else
            {
                activeWindow = null;
            }
        }

        public void NeedFun()
        {
            SceneManager.LoadScene("MinigameJump");
        }

        public void NeedHunger()
        {
            ResetUIWindows();
            if (activeWindow != _uiInventoryWindow)
            {
                _uiInventoryWindow.SetActive(true);
                activeWindow = _uiInventoryWindow;
            }
            else
            {
                activeWindow = null;
            }
        }
        
        public void NeedEnergy()
        {
            PetNeedEnergy energy = Player.Instance.Pet.Energy;
            energy.IsSleeping = !energy.IsSleeping;
            Player.Instance.Save();
            FindObjectOfType<Ikimono>().SetSprite();
        }

        public void ResetUIWindows()
        {
            _uiInventoryWindow.SetActive(false);
            _uiShopWindow.SetActive(false);
            _uiSettingsWindow.SetActive(false);
            activeWindow = null;
        }

        public void DeleteSave()
        {
            // TODO: add confirmation dialog
            Player.Reset();
            Application.Quit();
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }
    }
}