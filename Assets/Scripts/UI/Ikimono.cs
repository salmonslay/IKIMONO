using UnityEngine;
using IKIMONO.Pet;

namespace IKIMONO.UI
{
    public class Ikimono : MonoBehaviour
    {
        [SerializeField] private GameObject _sleepFx;

        private Player _player;
        private Animator _animator;
        
        private void Awake()
        {
            _player = Player.Instance;
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            SetSprite();
            PetNeed.ValueUpdated += SetSprite;
        }

        private void OnDestroy()
        {
            PetNeed.ValueUpdated -= SetSprite;
        }

        public void SetSprite()
        {
            _sleepFx.SetActive(false);
            
            if (_player.Pet.Energy.IsSleeping)
            {
                _animator.Play("Sleep", 0, 1);

                AudioManager.Instance.PlaySound("Sleeping", "One");
                _sleepFx.SetActive(true);
            }
            else if (_player.Pet.Overall.Percentage < 0.3f)
            {
                _animator.Play("Sad", 0, 1);
            }
            else
            {
                _animator.Play("Idle", 0, 1);
            }
        }
    }
}