using UnityEngine;
using UnityEngine.Audio;

namespace IKIMONO.Minigame.Jump
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private AudioClip _coinSound;
        [SerializeField] private AudioMixerGroup _platformMixer;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Player")) return;
            
            AudioSource source = GameManager.PlayAudio(_coinSound);
            source.pitch = Random.Range(0.9f, 1.1f);
            source.outputAudioMixerGroup = _platformMixer;
            
            JumpManager.Instance.AddCoin();
            
            Destroy(gameObject);
        }
    }
}