using UnityEngine;

namespace IKIMONO.Minigame.Jump
{
    public class Coin : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Player")) return;

            // @PhilipAudio: Play coin pick up sound here
            AudioManager.Instance.randomizeSound("Coin");
            
            JumpManager.Instance.AddCoin();
            
            Destroy(gameObject);
        }
    }
}