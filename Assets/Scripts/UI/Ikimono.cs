using UnityEngine;
using IKIMONO.Pet;

namespace IKIMONO.UI
{
    public class Ikimono : MonoBehaviour
    {
        [SerializeField] private GameObject _sleepFx;
        [SerializeField] Background _background;

        private Player _player;
        private static readonly int IsHappy = Animator.StringToHash("IsHappy");
        private static readonly int IsSleeping = Animator.StringToHash("IsSleeping");
        public static Animator Animator { get; private set; }
        
        private void Awake()
        {
            _player = Player.Instance;
            Animator = GetComponent<Animator>();
        }

        private void Start()
        {
            AudioManager.Instance.StopSound("GameAmb", "One");

            if(_player.Pet.Energy.IsSleeping == true)
            {
                AudioManager.Instance.PlaySound("SleepMusic", "One");
            }
            else
            {
                AudioManager.Instance.PlaySound("Music", "One");
            }

            if(Background.IsDay)
            {
                AudioManager.Instance.PlaySound("DayAmb", "One");
            }
            else if (Background.IsDay == false)
            {
                AudioManager.Instance.PlaySound("NightAmb", "One");

            }
         

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
                AudioManager.Instance.PlaySound("Sleeping", "One");
               
                _sleepFx.SetActive(true);
            }

            else if (_player.Pet.Overall.Percentage < 0.3f)
            {
              
            }
            else
            {
                
               
            }

            
            Animator.SetBool(IsHappy, _player.Pet.Overall.Percentage > 0.3f);
            Animator.SetBool(IsSleeping, _player.Pet.Energy.IsSleeping);

        }
    }
}