using System;
using UnityEngine;

namespace IKIMONO.UI
{

    public class ButtonManager : MonoBehaviour
    {
        [SerializeField] private GameObject _uiInventoryWindow;
        [SerializeField] private GameObject _uiShopWindow;
        [SerializeField] private GameObject _uiSettingsWindow;
        public void OpenSettings()
        {
            ResetUIWindows();
            _uiSettingsWindow.SetActive(true);
        }

        public void OpenShop()
        {
            ResetUIWindows();
            _uiShopWindow.SetActive(true);
        }

        public void NeedFun()
        {
            throw new NotImplementedException();
        }

        public void NeedHunger()
        {
            ResetUIWindows();
            _uiInventoryWindow.SetActive(true);
        }

        public void NeedHygiene()
        {
            throw new NotImplementedException();
        }

        public void NeedEnergy()
        {
            throw new NotImplementedException();
        }

        private void ResetUIWindows()
        {
            _uiInventoryWindow.SetActive(false);
            _uiShopWindow.SetActive(false);
            _uiSettingsWindow.SetActive(false);
        }
    }
}