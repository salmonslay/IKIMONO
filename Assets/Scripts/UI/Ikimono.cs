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
        [SerializeField] private GameObject _sleepFx;

        private Player _player;
        private Image _image;
        private void Awake()
        {
            _player = Player.Instance;
            _image = GetComponent<Image>();

        }

        private void Start()
        {
            SetSprite();
            PetNeed.ValueUpdated += SetSprite;


            AudioManager.Instance.PlaySound("Music", "Two");
            AudioManager.Instance.PlaySound("DayAmb", "One");  // när det är dag



        }

        private void OnDestroy()
        {
            PetNeed.ValueUpdated -= SetSprite;
        }

        // @PhilipAudio: Put the snore sounds in this class. They should be looped with randomized intervals,
        // and you can use Player.Instance.Pet.Energy.IsSleeping to verify whether or not the pet is sleeping.
        // This is not event based, so you can use it in Update.

        public void SetSprite()
        {
            if (_player.Pet.Energy.IsSleeping)
            {
                _image.sprite = _sleeping;

                AudioManager.Instance.PlaySound("Sleeping", "One");
                AudioManager.Instance.StopSound("Music", "Two");
                // AudioManager.Instance.PlaySound("Music", "One");  // FEL PÅ LJUDHELVETET, spela lullabyen här
                _sleepFx.SetActive(true);


            }
            else if (_player.Pet.Overall.Percentage < 0.3f)
            {
                _image.sprite = _sad;

                _sleepFx.SetActive(false);
            }
            else
            {
                _image.sprite = _idle;
              


                _sleepFx.SetActive(false);
            }
        }
    }
}