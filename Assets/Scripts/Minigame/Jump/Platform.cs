using UnityEngine;
using Random = UnityEngine.Random;

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

        private const float CoinOdds = 0.1f;

        private void Awake()
        {
            if (Random.value < CoinOdds)
            {
                _coin.SetActive(true);
            }
        }

        private void Update()
        {
            if(JumpManager.Instance.Player.transform.position.y > transform.position.y + 8f)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!(collision.relativeVelocity.y <= 0f)) return;
            if (!collision.gameObject.CompareTag("Player")) return;
            
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 velocity = rb.velocity;
            velocity.y = 10.2f; // slightly above 10 to make sure it reaches
            rb.velocity = velocity;
        }
    }
}