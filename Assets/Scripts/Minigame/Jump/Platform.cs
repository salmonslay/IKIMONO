using UnityEngine;

namespace IKIMONO.Minigame.Jump
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private GameObject _coin;
        
        public const float CoinOdds = 0.1f;

        private void Awake()
        {
            if (Random.value < CoinOdds)
            {
                _coin.SetActive(true);
            }
        }
    }
}