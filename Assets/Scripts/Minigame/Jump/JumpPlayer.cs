using System;
using UnityEngine;

namespace IKIMONO.Minigame.Jump
{
    public class JumpPlayer : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Vector3 _screenBounds;
        
        /// <summary>
        /// The first jump is always manual. If this is true, this jump has been triggered by the player.
        /// </summary>
        private bool _hasJumped = false;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _screenBounds = Camera.main!.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        }

        private void Update()
        {
            // Use tilt if possible, but fallback to keyboard input
            float horizontalControl = Input.GetAxis("Horizontal");
            float horizontalTilt = Input.acceleration.x * 2;
            Vector2 move = horizontalControl != 0 ? new Vector2(horizontalControl, 0) : new Vector2(horizontalTilt, 0);
            
            transform.Translate(Time.deltaTime * 5 * move);
            
            // The first jump is always manual
            if (Input.GetMouseButtonDown(0) && !_hasJumped || Input.GetKeyDown(KeyCode.Space) && Application.isEditor)
            {
                Jump();
            }

            // Teleport the player to the other side of the screen if they go off screen
            Vector3 pos = transform.position;
            if (pos.x > _screenBounds.x)
                transform.position = new Vector3(-_screenBounds.x, pos.y, pos.z);
            else if (pos.x < -_screenBounds.x)
                transform.position = new Vector3(_screenBounds.x, pos.y, pos.z);
            
            // Rotate the player to face the direction they are moving in
            // This is slightly off-set to avoid lots of changes from small movements
            if (move.x < -0.1f)
                transform.localScale = new Vector3(-1, 1, 1);
            else if (move.x > 0.1f)
                transform.localScale = new Vector3(1, 1, 1);

            if (_rigidbody2D != null && _rigidbody2D.velocity.y < -20)
            {
                JumpManager.Instance.GameOver();
                Destroy(_rigidbody2D);
            }
        }

        public bool IsGrounded()
        {
            /*
            if (_rigidbody2D.velocity.y > 0)
            {
                return false;
            }

            Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, 0.2f);
            foreach (Collider2D collider in colliders)
            {
                if (!collider.CompareTag("Player") && !collider.isTrigger)
                    return true;
            }
            */
            return false;
        }

        private void Jump()
        {
            // This technically doesn't require a grounded check, but it might be good to have? Eh let's skip that for now
            _rigidbody2D.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            _hasJumped = true;
        }
    }
}