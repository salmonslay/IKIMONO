using UnityEngine;

namespace IKIMONO.Minigame.Jump
{
    public class JumpPlayer : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // TODO Tova: Use device tilting instead of keyboard input
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), 0);
            transform.Translate(move * Time.deltaTime * 5);
            
            // TODO Tova: Jump automatically when the player is on the ground
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
        }
        
        private void Jump()
        {
            // This technically doesn't require a grounded check, but it might be good to have? Eh let's skip that for now
            _rigidbody2D.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        }
    }
}