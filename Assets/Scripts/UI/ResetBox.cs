using System;
using IKIMONO.Pet;
using UnityEngine;
using UnityEngine.UI;

namespace IKIMONO.UI
{
    public class ResetBox : MonoBehaviour
    {
        [SerializeField] private Text _text;
        private const string DeletePetText = "You are about to delete all your progress, and put your pet up for adoption. This can not be undone. Are you sure?";
        private const string PetDeletedText = "A new owner for your pet has been found. Please restart the game.";

        public static void Open()
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/UI/ResetBox"));
        }
        
        public void OnClick()
        {
            Player.Reset();
            _text.text = PetDeletedText;

            foreach (Button button in GetComponentsInChildren<Button>())
            {
                button.interactable = false;
            }
        }
        
        public void OnClose()
        {
            Destroy(gameObject);
        }
    }
}