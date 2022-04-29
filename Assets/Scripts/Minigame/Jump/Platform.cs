using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace IKIMONO.Minigame.Jump
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private GameObject _coin;
        [SerializeField] private GameObject _platform;
        [SerializeField] private AudioClip _platformSound;
        [SerializeField] private AudioMixerGroup _platformMixer;
        private static float _coinOdds => 0.05f * JumpManager.Instance.Player.transform.position.y / 100;
        private Transform _camera;

        public void SetWidth(float width)
        {
            Vector3 scale = _platform.transform.localScale;
            scale.x = width;
            _platform.transform.localScale = scale;
        }

        private void Awake()
        {
            _camera = Camera.main!.gameObject.transform;
            
            if (Random.value < _coinOdds)
            {
                _coin.SetActive(true);
            }
        }

        private void Update()
        {
            if(_camera.position.y - transform.position.y > PlatformSpawner.ScreenBounds.y)
            {
                Destroy(transform.parent.gameObject);
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

            JumpManager.Instance.JumpCount++;

            AudioSource source = GameManager.PlayAudio(_platformSound);
            source.pitch = Random.Range(0.9f, 1.1f);
            source.outputAudioMixerGroup = _platformMixer;
        }
    }
}