using System;
using UnityEngine;
using IKIMONO.Pet;
using UnityEngine.UI;

namespace IKIMONO.UI
{
    public class Ikimono : MonoBehaviour
    {
        [Header("Ikimono Sprites")]
        [SerializeField] private Sprite _idle;
        [SerializeField] private Sprite _sad;
        [SerializeField] private Sprite _sleeping;
        
        private Player _player;
        private Image _image;
        private void Awake()
        {
            _player = Player.Instance;
            _image = GetComponent<Image>();
            
            SetSprite();
        }

        public void SetSprite()
        {
            if (_player.Pet.Energy.IsSleeping)
            {
                _image.sprite = _sleeping;   
            }
            else if (_player.Pet.Overall.Percentage < 0.3f)
            {
                _image.sprite = _sad;
            }
            else
            {
                _image.sprite = _idle;
            }
        }
    }
}