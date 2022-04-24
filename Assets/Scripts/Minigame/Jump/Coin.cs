using UnityEngine;

namespace IKIMONO.Minigame.Jump
{
    public class Coin : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Player")) return;
            JumpManager.Instance.AddCoin();
            Destroy(gameObject);
        }
    }
}