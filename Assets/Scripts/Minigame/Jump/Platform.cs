using UnityEngine;

namespace IKIMONO.Minigame.Jump
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private GameObject _coin;
        [SerializeField] private GameObject _platform;

        public void SetWidth(float width)
        {
            Vector3 scale = _platform.transform.localScale;
            scale.x = width;
            _platform.transform.localScale = scale;
        }

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