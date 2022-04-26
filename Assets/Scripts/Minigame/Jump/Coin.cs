using UnityEngine;

namespace IKIMONO.Minigame.Jump
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private AudioClip _coinSound;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Player")) return;
            
            GameManager.PlayAudio(_coinSound).pitch = Random.Range(0.9f, 1.1f);
            JumpManager.Instance.AddCoin();
            
            Destroy(gameObject);
        }
    }
}